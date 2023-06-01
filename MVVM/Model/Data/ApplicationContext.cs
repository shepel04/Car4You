using Car4You.MVVM.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace Car4You.MVVM.Model.Data
{
    class ApplicationContext : DbContext
    {
        public DbSet<CarDTO> Cars { get; set; }

        public ApplicationContext()
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-56B2CCP\\SQLEXPRESS;Database=Cars;Trusted_Connection=True;");
        }
    }
}
