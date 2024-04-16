using STUDY.ASP.ShiftLoggerTryThree.Models;

namespace STUDY.ASP.ShiftLoggerTryThree.Services
{
    public interface IShiftLoggerService
    {
        Task<List<ShiftLogger>> GetAllShiftLogs();
    }
}
