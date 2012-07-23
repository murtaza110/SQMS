using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SQMS.Web.Models
{
    public class SabaqRegistration
    {
        public SabaqRegistration()
        {
            this.SabaqAttendances = new HashSet<SabaqAttendance>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SabaqRegId { get; set; }

        public long SabaqGroupId { get; set; }
        
        public long MemberId { get; set; }

        [Required(ErrorMessage = "Please select at least one member")]
        public long[] SelectedMembers { get; set; }
        public IEnumerable<SelectListItem> AllMembers { get; set; }
        
        public short SabaqStatusId { get; set; }

        public virtual ICollection<SabaqAttendance> SabaqAttendances { get; set; }

        [ForeignKey("SabaqGroupId")]
        public virtual SabaqGroup SabaqGroup { get; set; }

        [ForeignKey("SabaqStatusId")]
        public virtual SabaqStatus SabaqStatus { get; set; }

        [ForeignKey("MemberId")]
        public virtual User User { get; set; }
    }
}