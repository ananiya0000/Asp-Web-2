using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assig.Models
{
    public class Machine
    {
        [Key]
        public int MachineID { get; set; }
        public string MachineName { get; set; }
        public double Price { get; set; }
        public string Industry { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string ImageUrl { get; set; }
        public bool Available { get; set; }
    }
}