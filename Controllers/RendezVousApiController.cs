using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaccinationCenter.Models;
using VaccinationCenter.Services;

namespace VaccinationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RendezVousApiController : ControllerBase
    {
        private readonly IRendezVousService _rdvService;

        public RendezVousApiController(IRendezVousService rdvService)
        {
            _rdvService = rdvService;
        }

        /// GET api/rendezVousApi
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _rdvService.GetAllWithDetailsAsync();
            return Ok(list.Select(r => new
            {
                r.RendezVousId,
                r.CodeInfirmiere,
                r.DateVaccination,
                r.NbrDoses,
                Citoyen = r.Citoyen == null ? null : new
                {
                    r.Citoyen.CiToyenId,
                    r.Citoyen.Nom,
                    r.Citoyen.Prenom,
                    r.Citoyen.CIN
                },
                Vaccin = r.Vaccin == null ? null : new
                {
                    r.Vaccin.VaccinId,
                    r.Vaccin.Fournisseur,
                    TypeVaccin = r.Vaccin.TypeVaccin.ToString()
                }
            }));
        }

        /// GET api/rendezVousApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rdv = await _rdvService.GetByIdAsync(id);
            if (rdv == null) return NotFound(new { message = "RDV introuvable" });
            return Ok(rdv);
        }

        /// GET api/rendezVousApi/citoyen/5
        [HttpGet("citoyen/{citoyenId}")]
        public async Task<IActionResult> GetByCitoyen(int citoyenId)
        {
            var list = await _rdvService.GetByCitoyenAsync(citoyenId);
            return Ok(list);
        }

        /// POST api/rendezVousApi
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RendezVous rdv)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _rdvService.CreateAsync(rdv);
            return CreatedAtAction(nameof(GetById),
                new { id = created.RendezVousId }, created);
        }

        /// DELETE api/rendezVousApi/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _rdvService.DeleteAsync(id);
            if (!result) return NotFound(new { message = "RDV introuvable" });
            return NoContent();
        }
    }
}