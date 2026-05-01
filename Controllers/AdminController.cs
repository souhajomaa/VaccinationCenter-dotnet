using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaccinationCenter.Models;
using VaccinationCenter.Services;

namespace VaccinationCenter.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IStatistiquesService _statsService;
        private readonly IVaccinService _vaccinService;
        private readonly ICitoyenService _citoyenService;
        private readonly IRendezVousService _rendezVousService;
        private readonly IGenericRepository<CentreVaccination> _centreService;
        private readonly ICompteService _compteService;

        public AdminController(
            IStatistiquesService statsService,
            IVaccinService vaccinService,
            ICitoyenService citoyenService,
            IRendezVousService rendezVousService,
            IGenericRepository<CentreVaccination> centreService,
            ICompteService compteService)
        {
            _statsService = statsService;
            _vaccinService = vaccinService;
            _citoyenService = citoyenService;
            _rendezVousService = rendezVousService;
            _centreService = centreService;
            _compteService = compteService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var stats = await _statsService.GetStatistiquesAsync();
            return View(stats);
        }

        // ========== VACCINS CRUD ==========
        public async Task<IActionResult> Vaccins() => View(await _vaccinService.GetWithCentreAsync());

        [HttpGet] public async Task<IActionResult> CreateVaccin()
        {
            ViewBag.Centres = await _centreService.GetAllAsync();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVaccin(Vaccin vaccin)
        {
            if (!ModelState.IsValid) { ViewBag.Centres = await _centreService.GetAllAsync(); return View(vaccin); }
            await _vaccinService.CreateAsync(vaccin);
            TempData["Success"] = "Vaccin ajouté avec succès";
            return RedirectToAction("Vaccins");
        }

        [HttpGet] public async Task<IActionResult> EditVaccin(int id)
        {
            var vaccin = await _vaccinService.GetByIdAsync(id);
            if (vaccin == null) return NotFound();
            ViewBag.Centres = await _centreService.GetAllAsync();
            return View(vaccin);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVaccin(Vaccin vaccin)
        {
            if (!ModelState.IsValid) { ViewBag.Centres = await _centreService.GetAllAsync(); return View(vaccin); }
            await _vaccinService.UpdateAsync(vaccin);
            TempData["Success"] = "Vaccin modifié avec succès";
            return RedirectToAction("Vaccins");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVaccin(int id)
        {
            await _vaccinService.DeleteAsync(id);
            TempData["Success"] = "Vaccin supprimé";
            return RedirectToAction("Vaccins");
        }

        // ========== CITOYENS CRUD ==========
        public async Task<IActionResult> Citoyens() => View(await _citoyenService.GetWithAddresseAsync());

        [HttpGet] public IActionResult CreateCitoyen() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCitoyen(Citoyen citoyen, Addresse addresse)
        {
            if (!ModelState.IsValid) return View(citoyen);
            citoyen.Addresse = addresse;
            await _citoyenService.CreateAsync(citoyen);
            TempData["Success"] = "Citoyen ajouté avec succès";
            return RedirectToAction("Citoyens");
        }

        [HttpGet] public async Task<IActionResult> EditCitoyen(int id)
        {
            var citoyen = await _citoyenService.GetByIdAsync(id);
            if (citoyen == null) return NotFound();
            return View(citoyen);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCitoyen(Citoyen citoyen)
        {
            if (!ModelState.IsValid) return View(citoyen);
            await _citoyenService.UpdateAsync(citoyen);
            TempData["Success"] = "Citoyen modifié avec succès";
            return RedirectToAction("Citoyens");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCitoyen(int id)
        {
            await _citoyenService.DeleteAsync(id);
            TempData["Success"] = "Citoyen supprimé";
            return RedirectToAction("Citoyens");
        }

        // ========== CENTRES CRUD ==========
        public async Task<IActionResult> Centres() => View(await _centreService.GetAllAsync());

        [HttpGet] public IActionResult CreateCentre() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCentre(CentreVaccination centre)
        {
            if (!ModelState.IsValid) return View(centre);
            await _centreService.CreateAsync(centre);
            TempData["Success"] = "Centre ajouté avec succès";
            return RedirectToAction("Centres");
        }

        [HttpGet] public async Task<IActionResult> EditCentre(int id)
        {
            var centre = await _centreService.GetByIdAsync(id);
            if (centre == null) return NotFound();
            return View(centre);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCentre(CentreVaccination centre)
        {
            if (!ModelState.IsValid) return View(centre);
            await _centreService.UpdateAsync(centre);
            TempData["Success"] = "Centre modifié avec succès";
            return RedirectToAction("Centres");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCentre(int id)
        {
            await _centreService.DeleteAsync(id);
            TempData["Success"] = "Centre supprimé";
            return RedirectToAction("Centres");
        }

        // ========== RENDEZ-VOUS ==========
        public async Task<IActionResult> RendezVous() => View(await _rendezVousService.GetAllWithDetailsAsync());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRendezVous(int id)
        {
            await _rendezVousService.DeleteAsync(id);
            TempData["Success"] = "Rendez-vous supprimé";
            return RedirectToAction("RendezVous");
        }

        // ========== COMPTES ==========
        public async Task<IActionResult> Comptes() => View(await _compteService.GetAllAsync());
    }
}
