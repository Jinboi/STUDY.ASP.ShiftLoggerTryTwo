using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STUDY.ASP.ShiftLoggerTryTwo.Data;
using STUDY.ASP.ShiftLoggerTryTwo.Entities;
using STUDY.ASP.ShiftLoggerTryTwo.Services.ShiftLogger;

namespace STUDY.ASP.ShiftLoggerTryTwo.Controllers
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

        public async Task<ActionResult<List<ShiftLoggerEntity>>> GetAllShiftLoggers()
        {
            var shifts = await _context.ShiftLoggers.ToListAsync();               
            
            return Ok(shifts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftLoggerEntity>> GetShiftLogger(int id)
        {   
            var shift = await _context.ShiftLoggers.FindAsync(id);
            if (shift is null)
                return NotFound("ShiftLogger not found");

            return Ok(shift);
        }

        [HttpPost]
        public async Task<ActionResult<List<ShiftLoggerEntity>>> AddShift(ShiftLoggerEntity shift)
        {
            _context.ShiftLoggers.Add(shift);
            await _context.SaveChangesAsync();

            return Ok(await _context.ShiftLoggers.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<ShiftLoggerEntity>>> UpdateShiftLogger(ShiftLoggerEntity updatedShift)
        {
            var dbShift = await _context.ShiftLoggers.FindAsync(updatedShift.Id);
            if (dbShift is null)
                return NotFound("Shift not found.");

            dbShift.EmployeeId = updatedShift.EmployeeId;
            dbShift.ClockIn = updatedShift.ClockIn;
            dbShift.ClockOut = updatedShift.ClockOut;

            await _context.SaveChangesAsync();

            return Ok(await _context.ShiftLoggers.ToListAsync());
        }
        [HttpDelete]
        public async Task<ActionResult<List<ShiftLoggerEntity>>> DeleteShift(int id)
        {
            var result = _shiftLoggerService.DeleteShift(id);
            if (result is null)
                return Ok(result);
        }
    }
}
