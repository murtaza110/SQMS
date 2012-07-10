using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SQMS.Web.Models
{
    public class SabaqGroup
    {
        public SabaqGroup()
        {
            this.SabaqBooks = new HashSet<SabaqBook>();
            this.SabaqRegistrations = new HashSet<SabaqRegistration>();
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public long SabaqGroupId { get; set; }
        public string GroupName { get; set; }
        public int NisaabId { get; set; }
        public long MoallimId { get; set; }
        public int MohallaId { get; set; }
        public System.DateTime CreationDate { get; set; }
        public string WeekDays { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<byte> Duration { get; set; }
        public short SabaqStatusId { get; set; }

        public virtual Nisaab Nisaab { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<SabaqBook> SabaqBooks { get; set; }
        public virtual User User { get; set; }
        public virtual SabaqStatus SabaqStatus { get; set; }
        public virtual ICollection<SabaqRegistration> SabaqRegistrations { get; set; }
    }
}