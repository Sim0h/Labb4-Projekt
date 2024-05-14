using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLabb4
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }
        [Required]
        public string AppointmentName { get; set; }
        [Required]
        public DateTime? AppointmentStart { get; set; }
        [Required]
        public DateTime? AppointmentEnd { get; set; }
        public double AppointmentLength { get; set; }

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        public int CompanyID { get; set; }
        public Company Company { get; set; }
    }
}
