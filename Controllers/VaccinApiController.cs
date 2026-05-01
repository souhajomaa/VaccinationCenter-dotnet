using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaccinationCenter.Models;
using VaccinationCenter.Services;

namespace VaccinationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VaccinApiController : ControllerBase
    {
        private readonly IVaccinService _vaccinService;

        public VaccinApiController(IVaccinService vaccinService)
        {
            _vaccinService = vaccinService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vaccins = await _vaccinService.GetAvailableVaccinsAsync();
            return Ok(vaccins.Select(v => new
            {
                v.VaccinId,
                v.Fournisseur,
                v.TypeVaccin,
                v.Quantite,
                DateValidite = v.DateValidite.ToString("yyyy-MM-dd"),
                Centre = v.CentreVaccination?.ResponsableCentre
            }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vaccin = await _vaccinService.GetByIdAsync(id);
            if (vaccin == null) return NotFound();
            return Ok(vaccin);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? fournisseur, [FromQuery] TypeVaccin? type)
        {
            IEnumerable<Vaccin> results;
            if (type.HasValue)
                results = await _vaccinService.SearchByTypeAsync(type.Value);
            else if (!string.IsNullOrEmpty(fournisseur))
                results = await _vaccinService.SearchByFournisseurAsync(fournisseur);
            else
                results = await _vaccinService.GetAvailableVaccinsAsync();
            return Ok(results);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] Vaccin vaccin)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _vaccinService.CreateAsync(vaccin);
            return CreatedAtAction(nameof(GetById), new { id = created.VaccinId }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Vaccin vaccin)
        {
            if (id != vaccin.VaccinId) return BadRequest();
            if (!await _vaccinService.ExistsAsync(id)) return NotFound();
            var updated = await _vaccinService.UpdateAsync(vaccin);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _vaccinService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
