namespace STUDY.ASP.ShiftLoggerTryTwo.Entities
{
    public class ShiftLogger
    {
        public int Id { get; set; }
        public required int EmployeeId { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime ClockOut { get; set; }
    }
}
