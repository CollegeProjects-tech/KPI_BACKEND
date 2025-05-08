using Microsoft.EntityFrameworkCore;
using Models;


namespace KPI_BACKEND.Contest
{
    public class KpiMvcDBContext : DbContext
    {
        public KpiMvcDBContext(DbContextOptions<KpiMvcDBContext> options) : base(options)
        {
        }
        public DbSet<tbl_Login> dTOTests { get; set; }
    }
}
