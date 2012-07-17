using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SQMS.Web.Models
{
    public class Nisaab
    {
        public Nisaab()
        {
            this.Books = new HashSet<Book>();
            this.SabaqGroups = new HashSet<SabaqGroup>();
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int NisaabId { get; set; }

        [Display(Name = "Nisaab Name")]
        public string NisaabName { get; set; }

        [Display(Name = "Prerequiste Nisaab")]
        public Nullable<int> PrereqNisaabId { get; set; }

        [Display(Name = "Pre-requisite No. Of Books")]
        public Nullable<int> NumPrereqBooks { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<SabaqGroup> SabaqGroups { get; set; }
         
        [ForeignKey("PrereqNisaabId")] 
        public virtual Nisaab Nissab1 { get; set; }
    }
}