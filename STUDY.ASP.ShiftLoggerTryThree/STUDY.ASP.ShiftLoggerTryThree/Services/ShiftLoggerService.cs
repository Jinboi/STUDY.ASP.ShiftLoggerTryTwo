using STUDY.ASP.ShiftLoggerTryThree.Data;
using Microsoft.EntityFrameworkCore;

namespace STUDY.ASP.ShiftLoggerTryThree.Services
{
    public class ShiftLoggerService : IShiftLoggerService
    {
        private readonly DataContext _context;

        public ShiftLoggerService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ShiftLogger>> GetAllShiftLogs()
        {
            var shifts = await _context.ShiftLogs.ToListAsync();
            return shifts;
        }
    }
}
    