using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SQMS.Web.Models
{
    public class SabaqRegistration
    {
        public SabaqRegistration()
        {
            this.SabaqAttendances = new HashSet<SabaqAttendance>();
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public long SabaqRegId { get; set; }
        public long SabaqGroupId { get; set; }
        public long MemberId { get; set; }
        public short SabaqStatusId { get; set; }

        public virtual ICollection<SabaqAttendance> SabaqAttendances { get; set; }
        public virtual SabaqGroup SabaqGroup { get; set; }
        public virtual SabaqStatus SabaqStatu { get; set; }
        public virtual User User { get; set; }
    }
}