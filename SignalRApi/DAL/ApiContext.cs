using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SignalRApi.DAL
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options): base(options) { }

        //protected readonly IConfiguration Configuration;
        //public ApiContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        //}
        public DbSet<Visitor> Visitors { get; set; }
    }
}
