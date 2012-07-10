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
        public string Title { get; set; }
        public int NisaabId { get; set; }
        public Nullable<bool> IsCompulsory { get; set; }
        public string Author { get; set; }
        public Nullable<short> YearPublished { get; set; }
        public string Tags { get; set; }

        public virtual Nisaab Nisaab { get; set; }
        public virtual ICollection<SabaqBook> SabaqBooks { get; set; }
    }
}