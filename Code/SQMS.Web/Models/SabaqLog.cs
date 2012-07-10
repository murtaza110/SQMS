using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SQMS.Web.Models
{
    public class SabaqLog
    {
        public SabaqLog()
        {
            this.SabaqAttendances = new HashSet<SabaqAttendance>();
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public long SabaqLogId { get; set; }
        public long SabaqBookId { get; set; }
        public System.DateTime DateTIme { get; set; }
        public Nullable<int> VenueId { get; set; }
        public Nullable<byte> Duration { get; set; }

        public virtual ICollection<SabaqAttendance> SabaqAttendances { get; set; }
        public virtual SabaqBook SabaqBook { get; set; }
    }
}