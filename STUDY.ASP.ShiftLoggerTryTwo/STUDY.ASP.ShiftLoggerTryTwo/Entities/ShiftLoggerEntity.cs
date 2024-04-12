namespace STUDY.ASP.ShiftLoggerTryTwo.Entities
{
    public class ShiftLoggerEntity
    {
        public int Id { get; set; }
        public required int EmployeeId { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime ClockOut { get; set; }
    }
}
