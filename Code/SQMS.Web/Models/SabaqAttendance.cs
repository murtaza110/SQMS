using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SQMS.Web.Models
{
    public class SabaqAttendance
    {
        [Key, Column (Order = 0)]
        public long SabaqLogId { get; set; }

        [Required]
        [Key, Column(Order = 1)]
        public long SabaqRegId { get; set; }
        public Nullable<System.DateTime> SignInTime { get; set; }

        public virtual SabaqLog SabaqLog { get; set; }
        public virtual SabaqRegistration SabaqRegistration { get; set; }
    }
}