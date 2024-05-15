using ClassLibraryLabb4;
using System.Threading.Tasks;

namespace Labb4_Projekt.Services
{
    public interface ICompany<T>
    {
        Task<IEnumerable<CustomerDTO>> GetAllCustomers();
        Task<Customer> GetSingleCustomer(int id);
        Task<List<Appointment>> AppointmentsThisWeekAll(DateTime currentDate);
        Task<List<Appointment>> AppointmentsThisWeekCustomer(DateTime currentDate, int id);

        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer customer);
        Task<Customer> DeleteCustomer(int id);

        Task<Appointment> GetAppointmentByID(int id);
        Task<Appointment> AddAppointment(Appointment appointment);
        Task<Appointment> UpdateAppointment(Appointment entity);
        Task<Appointment> DeleteAppointment(int id);

        Task<IEnumerable<ChangeHistory>> GetChangeHistory();
        Task LogAppointmentChange(string changeType, DateTime? oldAppointmentTime, DateTime? newAppointmentTime, Appointment appointment);


    }
}
