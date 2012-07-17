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

        [Display(Name = "Halqa Name")]
        [Required]
        public string GroupName { get; set; }

        [Display(Name = "Nisaab")]
        [Required]
        public int NisaabId { get; set; }

        [Display(Name = "Moallim")]
        [Required]
        public long MoallimId { get; set; }

        [Required]
        public int MohallaId { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime CreationDate { get; set; }
        public string WeekDays { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime StartDate { get; set; }
        public Nullable<byte> Duration { get; set; }

        [Required]
        public short SabaqStatusId { get; set; }

        [ForeignKey("NisaabId")]
        public virtual Nisaab Nisaab { get; set; }

        [ForeignKey("MohallaId")]
        public virtual Region Region { get; set; }


        public virtual ICollection<SabaqBook> SabaqBooks { get; set; }

         [ForeignKey("MoallimId")]
        public virtual User User { get; set; }

         [ForeignKey("SabaqStatusId")]
        public virtual SabaqStatus SabaqStatus { get; set; }

        public virtual ICollection<SabaqRegistration> SabaqRegistrations { get; set; }
    }
}