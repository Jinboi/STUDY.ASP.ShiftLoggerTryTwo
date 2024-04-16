using STUDY.ASP.ShiftLoggerTryTwo.Entities;

namespace STUDY.ASP.ShiftLoggerTryTwo.Services.ShiftLogger
{
    public class ShiftLoggerService : IShiftLoggerService
    {
        public List<ShiftLoggerEntity> AddShift(ShiftLoggerEntity shift)
        {
            throw new NotImplementedException();
        }

        public List<ShiftLoggerEntity> DeleteShift(int id)
        {
            var dbShift = await _context.ShiftLoggers.FindAsync(id);
            if (dbShift is null)
                return NotFound("Shift not found.");

            _context.ShiftLoggers.Remove(dbShift);
            await _context.SaveChangesAsync();

            return Ok(await _context.ShiftLoggers.ToListAsync());
        }

        public List<ShiftLoggerEntity> GetAllShiftLoggers()
        {
            throw new NotImplementedException();
        }

        public ShiftLoggerEntity GetShiftLogger(int id)
        {
            throw new NotImplementedException();
        }

        public List<ShiftLoggerEntity> UpdateShiftLogger(ShiftLoggerEntity updatedShift)
        {
            throw new NotImplementedException();
        }
    }
}
