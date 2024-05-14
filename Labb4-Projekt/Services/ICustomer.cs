using ClassLibraryLabb4;
using Labb4_Projekt.Data;

namespace Labb4_Projekt.Services
{
    public interface ICustomer<T>
    {
        Task<Customer> GetSingleCustomer(int id);
        Task<Appointment> CancleAppointment(Appointment appointment);
        Task<Customer> UpdateCustomer(Customer customer);
        
        Task<Customer> GetUserWithAppointments1(int id);
        Task<Appointment> AddAppointment(Appointment appointment);
        Task LogAppointmentChange(string changeType, DateTime? oldAppointmentTime, DateTime? newAppointmentTime);
    }
}
