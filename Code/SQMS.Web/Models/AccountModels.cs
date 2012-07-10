using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace SQMS.Web.Models
{
    /*
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class User
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Ejamaat ID")]
        public Int64 UserId{ get; set;}

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password{get; set;}

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string FirstName{get; set;}

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }


    }*/

    /*
    public class Role
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Role Id")]
        public int RoleId { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

    }*/

     /* 
    public class UserRole
    {
        [Required]
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        [ForeignKey("RoleId")]
        [Display(Name = "Role Id")]
        public Role Role { get; set; }
    }*/

    /*
    public class Region
    {       
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name="Region Id")]
        public int RegionId { get; set; }

        //[ForeignKey("RegionTypeId")]
        //public RegionType RegionType { get; set; }

        //public short RegionTypeId { get; set; }

        [Required]
        [Display(Name = "Region Name")]
        public string RegionName { get; set; }

        [Display(Name="Parent Region Id")]
        public int ParentRegionId { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        
        //public virtual ICollection<SabaqGroup> SabaqGroups { get; set; }
        //public virtual ICollection<User> Users { get; set; }
    }
    */
}
