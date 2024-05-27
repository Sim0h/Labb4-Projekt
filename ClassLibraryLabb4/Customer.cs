using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLabb4
{
    public class Customer 
    {
        [Key]
        public int CustomerID { get; set; }
        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }
        [Required]
        [StringLength(50)]
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        [Required]
        public string CustomerPhone { get; set; }
        
        public string CustomerPassword { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
        public IdentityUser User { get; set; }
        public string IdentityUserId { get; set; }
    }
}
