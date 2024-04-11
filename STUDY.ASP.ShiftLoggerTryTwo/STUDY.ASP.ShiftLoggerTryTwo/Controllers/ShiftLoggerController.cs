using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STUDY.ASP.ShiftLoggerTryTwo.Data;
using STUDY.ASP.ShiftLoggerTryTwo.Entities;

namespace STUDY.ASP.ShiftLoggerTryTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftLoggerController : ControllerBase
    {
        private readonly DataContext _context;

        public ShiftLoggerController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<List<ShiftLogger>>> GetAllShiftLoggers()
        {
            var shifts = await _context.ShiftLoggers.ToListAsync();               
            
            return Ok(shifts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftLogger>> GetShiftLogger(int id)
        {   
            var shift = await _context.ShiftLoggers.FindAsync(id);
            if (shift is null)
                return NotFound("ShiftLogger not found");

            return Ok(shift);
        }

        [HttpPost]
        public async Task<ActionResult<List<ShiftLogger>>> AddShift(ShiftLogger shift)
        {
            _context.ShiftLoggers.Add(shift);
            await _context.SaveChangesAsync();

            return Ok(await _context.ShiftLoggers.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<ShiftLogger>>> UpdateShiftLogger(ShiftLogger updatedShift)
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
        public async Task<ActionResult<List<ShiftLogger>>> DeleteShift(int id)
        {
            var dbShift = await _context.ShiftLoggers.FindAsync(id);
            if (dbShift is null)
                return NotFound("Shift not found.");

            _context.ShiftLoggers.Remove(dbShift);
            await _context.SaveChangesAsync();

            return Ok(await _context.ShiftLoggers.ToListAsync());
        }
    }
}
