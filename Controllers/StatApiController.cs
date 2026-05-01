using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaccinationCenter.Services;

namespace VaccinationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatApiController : ControllerBase
    {
        private readonly IStatistiquesService _statsService;

        public StatApiController(IStatistiquesService statsService)
        {
            _statsService = statsService;
        }

        /// GET api/statApi
        [HttpGet]
        public async Task<IActionResult> GetStats()
        {
            var stats = await _statsService.GetStatistiquesAsync();
            return Ok(stats);
        }
    }
}