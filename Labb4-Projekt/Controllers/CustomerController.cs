using ClassLibraryLabb4;
using Labb4_Projekt.Data;
using Labb4_Projekt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Labb4_Projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomer<Customer> _customer;
        public CustomerController(ICustomer<Customer> customer)
        {
            _customer = customer;
        }

        [HttpPut("Update Customer/{id:int}")] //funkar
        public async Task<ActionResult<Customer>> UpdateCustomer(int id, Customer customer)
        {
            try
            {
                if (id != customer.CustomerID)
                {
                    return BadRequest("No Customer with that ID exists...");
                }
                var customerToUpdate = await _customer.GetSingleCustomer(id);
                if (customerToUpdate == null)
                {
                    return NotFound($"Customer with ID {id} could not be found..");
                }
                customer.CustomerID = id;
                var updatedCustomer = await _customer.UpdateCustomer(customer);
                if (updatedCustomer != null)
                {
                    return Ok(updatedCustomer);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update customer.");
                }
            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error to Post Data To Database.......");
            }
        } 

        [HttpDelete("Cancel Appointment/{customerID:int}/{id:int}")] //funkar
        public async Task<ActionResult<Appointment>> CancelAppointment(int id, int customerID)
        {
            try
            {

                var appointment = await _customer.GetUserWithAppointments1(customerID);
                if (appointment == null)
                {
                    return NotFound();
                }
                await _customer.LogAppointmentChange("Cancel", null, null);

                var result = await _customer.CancleAppointment(appointment.Appointments.FirstOrDefault(a => a.AppointmentID == id));
                if (result == null)
                {
                    return NotFound("This Appointment ID is not booked in your name.");
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to remove Data from Database.......");
            }
        } 

        [HttpPost("Book new appointment")]//funkar
        public async Task<ActionResult<Appointment>> BookNewAppointment(Appointment appointment)
        {


            try
            {
                if (appointment == null)
                {
                    return BadRequest();
                }
                var newAppointment = await _customer.AddAppointment(appointment);
                return CreatedAtAction(nameof(BookNewAppointment), new { id = newAppointment.AppointmentID }, newAppointment);

            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error to Post Data To Database.......");
            }

        } 

    }

}
