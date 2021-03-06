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
        public string Catagory { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        [Required]
        public byte[] Image { get; set; }
        [Required]
        public byte[] Image1 { get; set; }
        [Required]
        public byte[] Image2 { get; set; }
        public bool Available { get; set; }
    }
}