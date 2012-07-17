using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SQMS.Web.Models
{
    public class User
    {
        public User()
        {
            this.SabaqGroups = new HashSet<SabaqGroup>();
            this.SabaqRegistrations = new HashSet<SabaqRegistration>();
            this.Roles = new HashSet<Role>();
        }

        [Required]
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.None)]
        [Display(Name = "Ejamaat Id")]
        public long UserId { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Father Name")]
        public string LastName { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Address1")]
        public string Address1 { get; set; }

        [Display(Name = "Address2")]
        public string Address2 { get; set; }

        [Display(Name = "Phone1")]
        public string Phone1 { get; set; }

        [Display(Name = "Phone2")]
        public string Phone2 { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Mohallah")]
        public Nullable<int> MohallaId { get; set; }

        [Display(Name = "Secret Question")]
        public string SecurityQuestion { get; set; }

        [Display(Name = "Secret Answer")]
        public string SecurityAnswer { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }


        public bool IsActive { get; set; }

        [ForeignKey("MohallaId")]
        public virtual Region Region { get; set; }

        //[ForeignKey("SecurityQuestionId")]
        //public virtual SecurityQuestion SecurityQuestion { get; set; }

        public virtual ICollection<SabaqGroup> SabaqGroups { get; set; }
        public virtual ICollection<SabaqRegistration> SabaqRegistrations { get; set; }        
        public virtual ICollection<Role> Roles { get; set; }

        public string NameToShow
        {
            get
            {
                return Title + " " + FirstName + " " + LastName;
            }
        }

        public string UserID_Name
        {
            get
            {
                return UserId + " - " + NameToShow;
            }
        }
    }
}