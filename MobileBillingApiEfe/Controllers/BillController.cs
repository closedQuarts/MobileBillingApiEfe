using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileBillingApiEfe.DTOs;
using MobileBillingApiEfe.Services;

namespace MobileBillingApiEfe.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpPost("calculate")]
        [Authorize]
        public IActionResult Calculate([FromBody] BillRequestDTO dto)
        {
            var bill = _billService.CalculateBill(dto);
            return Ok(bill);
        }

        [HttpPost("pay")]
        public IActionResult Pay([FromBody] PayRequestDTO dto)
        {
            var message = _billService.PayBill(dto);
            return Ok(new { message });
        }

        [HttpGet("{subscriberId}")]
        public IActionResult GetBills(int subscriberId)
        {
            var bills = _billService.GetBillsBySubscriber(subscriberId);
            return Ok(bills);
        }

        [HttpGet("detailed")]
        [Authorize]
        public IActionResult GetDetailedBill(
            [FromQuery] int subscriberId,
            [FromQuery] int month,
            [FromQuery] int year,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var detailed = _billService.GetDetailedBill(subscriberId, month, year, page, pageSize);
            return Ok(detailed);
        }
    }
}
