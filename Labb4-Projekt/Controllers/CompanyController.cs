using ClassLibraryLabb4;
using Labb4_Projekt.Data;
using Labb4_Projekt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Labb4_Projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminPolicy")]
    public class CompanyController : ControllerBase
    {
        private ICompany<Company> _company;
        public CompanyController(ICompany<Company> company)
        {
            _company = company;
        }

        [HttpGet("Get all Customers")] //funkar
        public async Task<ActionResult<Customer>> GetAllCustomer()
        {
            try
            {
                return Ok(await _company.GetAllCustomers());

            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error to get Data from Database.......");
            }
        }

        [HttpGet("Get Customer by ID{id:int}")] //funkar
        public async Task<ActionResult<Customer>> GetCustomerByID(int id)
        {
            try
            {
                var result = await _company.GetSingleCustomer(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error to get Data from Database.......");
            }
        }

        [HttpDelete("Delete Customer")] //funkar
        public async Task<ActionResult<Customer>> RemoveCustomer(int id)
        {
            try
            {
                var result = await _company.DeleteCustomer(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error to remove Data from Database.......");
            }
        }

        [HttpPost("Add Customer")] //funkar
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest();
                }
                var newCustomer = await _company.AddCustomer(customer);
                return CreatedAtAction(nameof(AddCustomer), new { id = newCustomer.CustomerID }, newCustomer);
            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error to Post Data To Database.......");
            }
        }

        [HttpPut("Update Customer/{id:int}")] //funkar behöver CustomerID
        public async Task<ActionResult<Customer>> UpdateCustomer(int id, Customer customer)
        {
            try
            {
                if (id != customer.CustomerID)
                {
                    return BadRequest("No Customer with that ID exists...");
                }

                var customerToUpdate = await _company.GetSingleCustomer(id);
                if (customerToUpdate == null)
                {
                    return NotFound($"Customer with ID {id} could not be found..");
                }
                return await _company.UpdateCustomer(customer);
            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error to Post Data To Database.......");
            }
        }

        [HttpPost("Add Appointment")] //funkar behöver company + customer ID
        public async Task<ActionResult<Appointment>> AddAppointment(Appointment appointment)
        {
            try
            {
                if (appointment == null)
                {
                    return BadRequest();
                }
                var newAppointment = await _company.AddAppointment(appointment);
                await _company.LogAppointmentChange("Added Appointment by Company", null, null, newAppointment);
                return CreatedAtAction(nameof(AddAppointment), new { id = newAppointment.AppointmentID }, newAppointment);

            }
            catch 
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error to Post Data To Database.......");
            }
        }

        [HttpPut("Uppdate Appointment{id:int}")] //funkar behöver AppointmentID
        public async Task<ActionResult<Appointment>> UppdateAppointment(int id, Appointment appointment)
        {
            try
            {
                if (id != appointment.AppointmentID)
                {
                    return BadRequest("Appointment ID doesn't match..");
                }

                var appointmentToUpdate = await _company.GetAppointmentByID(id);
                if (appointmentToUpdate == null)
                {
                    return NotFound($"Appointment with ID={id} not found..");
                }

                var oldAppointmentTime = appointmentToUpdate.AppointmentStart;
                appointmentToUpdate.AppointmentName = appointment.AppointmentName;
                appointmentToUpdate.AppointmentLength = appointment.AppointmentLength;
                appointmentToUpdate.AppointmentStart = appointment.AppointmentStart;
                appointmentToUpdate.AppointmentEnd = appointment.AppointmentEnd;
                appointmentToUpdate.AppointmentID = appointment.AppointmentID;

                var updatedAppointment = await _company.UpdateAppointment(appointmentToUpdate);

                if (updatedAppointment == null)
                {
                    return NotFound($"Appointment with ID={id} not found..");
                }

                // Log the appointment change
                await _company.LogAppointmentChange("Updated Appointment by Company", oldAppointmentTime, appointment.AppointmentStart, appointment);

                return updatedAppointment;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating appointment.");
            }
        }

        [HttpDelete("Delete Appointment{id:int}")] //funkar 
        public async Task<ActionResult<Appointment>> DeleteAppointment(int id)
        {
            try
            {
                var result = await _company.DeleteAppointment(id);
                if (result == null)
                {
                    return NotFound();
                }
                await _company.LogAppointmentChange("Removed Appointment by Company", null, null, result);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error to remove Data from Database.......");
            }
        }

        [HttpGet("Get Appointments this week")] //funkar
        public async Task<ActionResult<List<Appointment>>> GetAppointmentsThisWeek()
        {
            try
            {
                var currentDate = DateTime.Now;
                var appointmentsThisWeek = await _company.AppointmentsThisWeekAll(currentDate);
                return Ok(appointmentsThisWeek);
            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error to get Data from Database......." );
            }
        }

        [HttpGet("Get Customer Appointments this week{id:int}")] // funkar
        public async Task<ActionResult<List<Appointment>>> GetAppointmentByCustomer(int id)
        {
            try
            {
                var currentDate = DateTime.Now;
                var appointmentsForCustomer = await _company.AppointmentsThisWeekCustomer(currentDate, id);
                return Ok(appointmentsForCustomer);
            }
            catch 
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error to get Data from Database.......");
            }
        }

        [HttpGet("Change History")] //funkar
        public async Task<ActionResult<IEnumerable<ChangeHistory>>> GetChangeHistory()
        {
            try
            {
                var changeHistory = await _company.GetChangeHistory();
                return Ok(changeHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving change history: {ex.Message}");
            }
        }
    }
}

