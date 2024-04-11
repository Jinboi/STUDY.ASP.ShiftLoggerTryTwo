using Microsoft.EntityFrameworkCore;
using STUDY.ASP.ShiftLoggerTryTwo.Entities;

namespace STUDY.ASP.ShiftLoggerTryTwo.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<ShiftLogger> ShiftLoggers { get; set; }
    }
}
