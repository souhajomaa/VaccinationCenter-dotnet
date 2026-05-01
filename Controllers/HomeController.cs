using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaccinationCenter.Models;
using VaccinationCenter.Services;
using VaccinationCenter.ViewModels;

namespace VaccinationCenter.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IVaccinService _vaccinService;
        private readonly IRendezVousService _rendezVousService;
        private readonly ICitoyenService _citoyenService;

        public HomeController(IVaccinService vaccinService, IRendezVousService rendezVousService, ICitoyenService citoyenService)
        {
            _vaccinService = vaccinService;
            _rendezVousService = rendezVousService;
            _citoyenService = citoyenService;
        }

        public async Task<IActionResult> Index()
        {
            var vaccins = await _vaccinService.GetAvailableVaccinsAsync();
            return View(vaccins);
        }

        public async Task<IActionResult> Search(VaccinSearchViewModel model)
        {
            IEnumerable<Vaccin> results;

            if (model.Date.HasValue)
                results = await _vaccinService.SearchByDateAsync(model.Date.Value);
            else if (model.Type.HasValue)
                results = await _vaccinService.SearchByTypeAsync(model.Type.Value);
            else if (!string.IsNullOrEmpty(model.Fournisseur))
                results = await _vaccinService.SearchByFournisseurAsync(model.Fournisseur);
            else
                results = await _vaccinService.GetAvailableVaccinsAsync();

            model.Results = results;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Reserver(int id)
        {
            var vaccin = await _vaccinService.GetByIdAsync(id);
            if (vaccin == null) return NotFound();

            var model = new ReservationViewModel
            {
                VaccinId = id,
                Vaccin = vaccin
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserver(ReservationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Vaccin = await _vaccinService.GetByIdAsync(model.VaccinId);
                return View(model);
            }

            var citoyen = await _citoyenService.GetByCINAsync(model.CIN);
            if (citoyen == null)
            {
                ModelState.AddModelError("CIN", "Citoyen introuvable avec ce CIN");
                model.Vaccin = await _vaccinService.GetByIdAsync(model.VaccinId);
                return View(model);
            }

            var rdv = new RendezVous
            {
                VaccinId = model.VaccinId,
                CiToyenId = citoyen.CiToyenId,
                DateVaccination = model.DateVaccination,
                CodeInfirmiere = model.CodeInfirmiere,
                NbrDoses = model.NbrDoses
            };

            await _rendezVousService.CreateAsync(rdv);

            // Decrease quantity
            var vaccin = await _vaccinService.GetByIdAsync(model.VaccinId);
            if (vaccin != null)
            {
                vaccin.Quantite -= model.NbrDoses;
                await _vaccinService.UpdateAsync(vaccin);
            }

            TempData["Success"] = "Rendez-vous réservé avec succès !";
            return RedirectToAction("Index");
        }
    }
}
