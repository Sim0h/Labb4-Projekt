using Labb4_Projekt.Data;
using System.Runtime.CompilerServices;
using ClassLibraryLabb4;
using Microsoft.EntityFrameworkCore;

namespace Labb4_Projekt.Services
{
    public class CustomerRepo : ICustomer<Customer>
    {
        private AppDbContext _appDbContext;
        public CustomerRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Appointment> UpdateAppointment(int id, Appointment appointment)
        {
            var result = await _appDbContext.Appointments.FirstOrDefaultAsync(a => a.AppointmentID == id && a.CustomerID == appointment.CustomerID);
            if (result != null)
            {
                result.AppointmentStart = appointment.AppointmentStart;
                result.AppointmentEnd = appointment.AppointmentEnd;
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        } //funkar

      

        public async Task<Customer> GetSingleCustomer(int id)
        {
            return await _appDbContext.Customers.FirstOrDefaultAsync(c => c.CustomerID == id);
        } //funkar

        public async Task<Appointment> CancleAppointment(Appointment appointment)
        {
            var result = await _appDbContext.Appointments.FirstOrDefaultAsync(a => a.AppointmentID == appointment.AppointmentID);
            if (result != null)
            {
                _appDbContext.Appointments.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        } //funkar

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            var existingCustomer = await _appDbContext.Customers.FirstOrDefaultAsync(c => c.CustomerID == customer.CustomerID);

            if (existingCustomer != null)
            {
                
                existingCustomer.CustomerName = customer.CustomerName;
                existingCustomer.CustomerEmail = customer.CustomerEmail;
                existingCustomer.CustomerPhone = customer.CustomerPhone;
                existingCustomer.CustomerAddress = customer.CustomerAddress;

                await _appDbContext.SaveChangesAsync();
                return existingCustomer;
            }
            return null;
        } //funkar


        public async Task<Customer> GetUserWithAppointments1(int id)
        {
            var customer = await _appDbContext.Customers.Include(c => c.Appointments)
                                                        .FirstOrDefaultAsync(c => c.CustomerID == id);
            return customer;
        } //funkar

        public async Task<Appointment> AddAppointment(Appointment appointment)
        {
            var result = await _appDbContext.Appointments.AddAsync(appointment);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        } //funkar

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
        } //funkar
    }
}


/*
10. Kundinformation bör skyddas och endast tillgänglig för auktoriserade användare.
 */