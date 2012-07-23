using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SQMS.Web.Models
{
    public class SabaqStatus
    {
        public SabaqStatus()
        {
            this.SabaqGroups = new HashSet<SabaqGroup>();
            this.SabaqRegistrations = new HashSet<SabaqRegistration>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short SabaqStatusId { get; set; }

        [Column("SabaqStatus")]
        public string SabaqStatusName { get; set; }
    
        public virtual ICollection<SabaqGroup> SabaqGroups { get; set; }
        public virtual ICollection<SabaqRegistration> SabaqRegistrations { get; set; }
    }
}