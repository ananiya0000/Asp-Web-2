using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assig.Models
{
    public class Notification
    {
        [Key]
        public int NotificationID { get; set; }
        public string CustomerEmail { get; set; }
        public string OwnerEmail { get; set; }
        public int MachineID { get; set; }
        public virtual Machine Machine { get; set; }
        public bool Seen { get; set; }
        public Nullable<DateTime> RequestDate { get; set; }
    }
}