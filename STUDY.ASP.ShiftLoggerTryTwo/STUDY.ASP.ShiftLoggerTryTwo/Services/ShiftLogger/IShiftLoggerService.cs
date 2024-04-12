using STUDY.ASP.ShiftLoggerTryTwo.Entities;

namespace STUDY.ASP.ShiftLoggerTryTwo.Services.ShiftLogger
{
    public interface IShiftLoggerService
    {
        List<ShiftLoggerEntity> GetAllShiftLoggers();

        ShiftLoggerEntity GetShiftLogger(int id);

        List<ShiftLoggerEntity> AddShift(ShiftLoggerEntity shift);

        List<ShiftLoggerEntity> UpdateShiftLogger(ShiftLoggerEntity updatedShift);

        List<ShiftLoggerEntity> DeleteShift(int id);

    }
}