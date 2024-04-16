using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STUDY.ASP.ShiftLoggerTryThree.Models;
using STUDY.ASP.ShiftLoggerTryThree.Services;


namespace STUDY.ASP.ShiftLoggerTryThree.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftLoggerController : ControllerBase
    {

        private readonly IShiftLoggerService _shiftLoggerService;

        public ShiftLoggerController(IShiftLoggerService shiftLoggerService)
        {
            _shiftLoggerService = shiftLoggerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ShiftLogger>>> GetAllShiftLogs()
        {
            return await _shiftLoggerService.GetAllShiftLogs();
        }
    }
}
