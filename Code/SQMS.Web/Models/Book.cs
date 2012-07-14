using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SQMS.Web.Models
{
    public class Book
    {
        public Book()
        {
            this.SabaqBooks = new HashSet<SabaqBook>();
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [Display(Name = "Book Title")]
        [Required]
        [MaxLength(50, ErrorMessage = "Book Title cannot be greater than 50")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Corresponding Nisaab")]
        public int NisaabId { get; set; }
        public Nullable<bool> IsCompulsory { get; set; }
        public string Author { get; set; }
        public Nullable<short> YearPublished { get; set; }
        public string Tags { get; set; }

        [ForeignKey("NisaabId")]        
        public virtual Nisaab Nisaab { get; set; }
        public virtual ICollection<SabaqBook> SabaqBooks { get; set; }
    }
}