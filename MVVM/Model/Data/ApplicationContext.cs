using Car4You.MVVM.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace Car4You.MVVM.Model.Data
{
    class ApplicationContext : DbContext
    {
        public DbSet<CarDTO> Cars { get; set; }

        public ApplicationContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-56B2CCP\\SQLEXPRESS;Database=Carsdb;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
