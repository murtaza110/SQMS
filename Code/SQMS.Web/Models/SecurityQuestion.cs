using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SQMS.Web.Models
{
    public class SecurityQuestion
    {
        public SecurityQuestion()
        {
            this.Users = new HashSet<User>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short SecurityQuestionId { get; set; }

        [Column("SecurityQuestion")]
        public string SecurityQuestion1 { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}