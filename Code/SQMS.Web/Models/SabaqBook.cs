using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SQMS.Web.Models
{
    public class SabaqBook
    {
        public SabaqBook()
        {
            this.SabaqLogs = new HashSet<SabaqLog>();
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public long SabaqBookId { get; set; }
        public long SabaqGroupId { get; set; }
        public int BookId { get; set; }
        public string Status { get; set; }

        public virtual Book Book { get; set; }
        public virtual SabaqGroup SabaqGroup { get; set; }
        public virtual ICollection<SabaqLog> SabaqLogs { get; set; }
    }
}