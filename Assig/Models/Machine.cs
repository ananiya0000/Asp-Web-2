using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assig.Models
{
    public class Machine
    {
        [Key]
        public int MachineId { get; set; }
        public double Price { get; set; }
        public string Industry { get; set; }
        public string OwnerEmail { get; set; }
        public string City { get; set; }
        public bool Available { get; set; }
        public bool Approved { get; set; }
        public string Name { get; set; }
    }
}