using Microsoft.EntityFrameworkCore;
using ClassLibraryLabb4;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Labb4_Projekt.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ChangeHistory> ChangeHistorys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Test Data Customer

            modelBuilder.Entity<Company>().HasData(new Company
            {
                CompanyID = 1,
                CompanyName = "SUT23 Kliniken"
            });


            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerID = 1,
                CustomerName = "Jonas Eriksson",
                CustomerEmail = "jonas@gmail.se",
                CustomerPhone = "0712345432"
            });

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerID = 2,
                CustomerName = "Simon Ståhl",
                CustomerEmail = "simon@gmail.se",
                CustomerPhone = "0712345444"
            });

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerID = 3,
                CustomerName = "Göran Karlsson",
                CustomerEmail = "Göran@gmail.se",
                CustomerPhone = "0712345666"
            });



            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentID = 1,
                AppointmentName = "Läkarbesök",
                AppointmentStart = new DateTime(2024, 5, 29, 13, 0, 0),
                AppointmentEnd = new DateTime(2024, 5, 29, 15, 0, 0),
                AppointmentLength = 2,
                CustomerID = 2,
                CompanyID=1


            });

            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentID = 2,
                AppointmentName = "Hälsokontroll",
                AppointmentStart = new DateTime(2024, 5, 29, 09, 0, 0),
                AppointmentEnd = new DateTime(2024, 5, 31, 10, 0, 0),
                AppointmentLength = 1,
                CustomerID = 1,
                CompanyID = 1

            });

            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentID = 3,
                AppointmentName = "Rutinkontroll",
                AppointmentStart = new DateTime(2024, 5, 30, 13, 0, 0),
                AppointmentEnd = new DateTime(2024, 5, 30, 14, 0, 0),
                AppointmentLength = 1,
                CustomerID = 2,
                CompanyID = 1

            });

        }



    }
}
