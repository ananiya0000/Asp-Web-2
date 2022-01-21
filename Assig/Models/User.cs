using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Assig.Models
{
    public class User
    {
        [Key]
        public string userName { get; set; }
        public string Email { get; set; }
        public bool Blocked { get; set; }
        public int CurrentRentedMaterialId { get; set; }
        public virtual Machine Machine { get; set; }
    }
}