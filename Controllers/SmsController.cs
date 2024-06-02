using Microsoft.AspNetCore.Mvc;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly ISmsService _smsService;

        public SmsController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        [HttpPost("send")]
        public IActionResult Send(SendSmsDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _smsService.Send(dto.MobileNumber, dto.Body);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }
    }
}
