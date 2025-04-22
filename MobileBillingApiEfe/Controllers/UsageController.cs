using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileBillingApiEfe.DTOs;
using MobileBillingApiEfe.Services;

namespace MobileBillingApiEfe.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsageController : ControllerBase
    {
        private readonly IUsageService _usageService;

        public UsageController(IUsageService usageService)
        {
            _usageService = usageService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddUsage([FromBody] UsageDTO dto)
        {
            await _usageService.AddUsageAsync(dto);
            return Ok(new { message = "Kullanım başarıyla eklendi!" });
        }
    }
}
