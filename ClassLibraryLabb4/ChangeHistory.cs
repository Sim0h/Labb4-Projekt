using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLabb4
{
    public class ChangeHistory
    {
        [Key]
        public int ChangeID { get; set; }
        public string ChangeType { get; set; }
        public DateTime WhenChanged { get; set; }
        public DateTime? OldAppointmentTime { get; set; }
        public DateTime? NewAppointmentTime { get; set; }
        public int AppointmentID { get; set; }
        

    }
}
