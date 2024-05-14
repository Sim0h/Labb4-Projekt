using Labb4_Projekt.Data;
using System.Runtime.CompilerServices;
using ClassLibraryLabb4;
using Microsoft.EntityFrameworkCore;

namespace Labb4_Projekt.Services
{
    public class CompanyRepo : ICompany<Company>
    {
        private AppDbContext _appDbContext;
        public CompanyRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

               
        public async Task<Customer> UpdateCustomer(Customer entity)
        {
            var result = await _appDbContext.Customers.FirstOrDefaultAsync(c => c.CustomerID == entity.CustomerID);

            if (result != null)
            {
                result.CustomerName = entity.CustomerName;
                result.CustomerEmail = entity.CustomerEmail;
                result.CustomerPhone = entity.CustomerPhone;
                result.CustomerAddress = entity.CustomerAddress;

                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _appDbContext.Customers.ToListAsync();
        } 

        public async Task<Customer> GetSingleCustomer(int id)
        {
            return await _appDbContext.Customers.FirstOrDefaultAsync(c => c.CustomerID == id);
        }

        public async Task<List<Appointment>> AppointmentsThisWeekAll(DateTime currentDate)
        {
            DateTime startOfWeek = currentDate.Date.AddDays(-(int)currentDate.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(7).AddSeconds(-1);


            var appointmentsThisWeek = await _appDbContext.Appointments
                .Where(a => a.AppointmentStart >= startOfWeek && a.AppointmentEnd <= endOfWeek)
                .ToListAsync();


            return appointmentsThisWeek;
        }

        public async Task<List<Appointment>> AppointmentsThisWeekCustomer(DateTime currentDate, int id)
        {

            DateTime startOfWeek = currentDate.Date.AddDays(-(int)currentDate.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(7);


            var appointmentsThisWeekForCustomer = await _appDbContext.Appointments
                .Where(appointment => appointment.CustomerID == id &&
                                       appointment.AppointmentStart >= startOfWeek &&
                                       appointment.AppointmentEnd <= endOfWeek)
                .ToListAsync();

            return appointmentsThisWeekForCustomer;
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            var result = await _appDbContext.Customers.AddAsync(customer);
            await _appDbContext.SaveChangesAsync();
            return result.Entity; 
        } 

        public async Task<Customer> DeleteCustomer(int id)
        {
            var result = await _appDbContext.Customers.FirstOrDefaultAsync(c=>c.CustomerID == id);
            if (result != null)
            {
                _appDbContext.Customers.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        } 

        public async Task<Appointment> GetAppointmentByID(int id)
        {
            return await _appDbContext.Appointments.FirstOrDefaultAsync(c=>c.AppointmentID == id);
        }

        public async Task<Appointment> AddAppointment(Appointment appointment)
        {
            var result = await _appDbContext.Appointments.AddAsync(appointment);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Appointment> UpdateAppointment(Appointment entity)
        {
            var result = await _appDbContext.Appointments.FirstOrDefaultAsync(c => c.AppointmentID == entity.AppointmentID);
            if (result != null)
            {
                result.AppointmentName = entity.AppointmentName;
                result.AppointmentLength = entity.AppointmentLength;
                result.AppointmentStart = entity.AppointmentStart;
                result.AppointmentEnd = entity.AppointmentEnd;

                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Appointment> DeleteAppointment(int id)
        {
            var result = await _appDbContext.Appointments.FirstOrDefaultAsync(a => a.AppointmentID == id);
            if (result != null)
            {
                _appDbContext.Appointments.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }


        public async Task<IEnumerable<ChangeHistory>> GetChangeHistory()
        {
            return await _appDbContext.ChangeHistorys.ToListAsync();
        }


        public async Task LogAppointmentChange(string changeType, DateTime? oldAppointmentTime, DateTime? newAppointmentTime)
        {
            var changeHistory = new ChangeHistory
            {
                ChangeType = changeType,
                WhenChanged = DateTime.Now,
                OldAppointmentTime = oldAppointmentTime,
                NewAppointmentTime = newAppointmentTime
            };

            _appDbContext.ChangeHistorys.Add(changeHistory);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
