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

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerID = 1,
                CustomerName = "Sam Testing",
                CustomerAddress = "SolGatan 12B",
                CustomerEmail = "Sam@testing.se",
                CustomerPhone = "0712365987"
                
                

            });
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerID = 2,
                CustomerName = "Simon Ståhl",
                CustomerAddress = "Varbergsgatan 31",
                CustomerEmail = "Simon@ståhl.se",
                CustomerPhone = "0744556698"
               


            });
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerID = 3,
                CustomerName = "Henrik Johansson",
                CustomerAddress = "Storgatan 6",
                CustomerEmail = "Henrik@johansson.se",
                CustomerPhone = "0723647895"
               

            });


            // Test Data Appointment 
            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentID = 1,
                AppointmentName = "Standard Läkarbesök",
                AppointmentLength = 1,
                AppointmentStart = new DateTime(2024, 05, 08, 10, 30, 0),
                AppointmentEnd = new DateTime(2024, 05, 08, 11, 30, 0),
                CustomerID = 1,
                CompanyID = 1,

            });
            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentID = 2,
                AppointmentName = "Ögonkontroll",
                AppointmentLength = 3,
                AppointmentStart = new DateTime(2024, 05, 7, 13, 30, 0),
                AppointmentEnd = new DateTime(2024, 05, 7, 16, 30, 0),
                CustomerID = 2,
                CompanyID = 1,

            });
            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentID = 3,
                AppointmentName = "Tandläkarbesök",
                AppointmentLength = 0.5,
                AppointmentStart = new DateTime(2024, 05, 10, 8, 0,0),
                AppointmentEnd = new DateTime(2024, 05, 10, 8, 30, 0),
                CustomerID = 2,
                CompanyID = 1,

            });


            //Test Data Company

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CompanyID);
                entity.Property(e => e.CompanyName).IsRequired();

                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ChangeHistory>().HasData(new ChangeHistory
            {
                ChangeID = 1,
                ChangeType = "Ombokning",
                OldAppointmentTime = new DateTime(2024,05,14,10,30,00),
                NewAppointmentTime = new DateTime(2024,05,15,10,30,00),
                WhenChanged = new DateTime(2024,05,14,10,30,00)
            });
           

        }



    }
}
