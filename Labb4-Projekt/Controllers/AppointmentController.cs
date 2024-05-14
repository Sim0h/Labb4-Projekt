using ClassLibraryLabb4;
using Labb4_Projekt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labb4_Projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private IAppData<Appointment> _appointment;
        public AppointmentController(IAppData<Appointment> appointment)
        {
            _appointment = appointment;
        }

        [HttpGet("Get All Appointments")] 
        public async Task<ActionResult<Appointment>> GetAllAppointments()
        {
            try
            {
                return Ok(await _appointment.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error to get Data from Database.......");
            }
            
        }

        [HttpGet("Get Appointment by ID{id:int}")] 
        public async Task<ActionResult<Appointment>> GetAppointmentByID(int id)
        {
            try
            {
                var result = await _appointment.GetSingle(id);
                if (result == null)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error to get Data from Database.......");
            }
        }
    }
}
