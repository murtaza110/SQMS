using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SQMS.Web.Models
{
    public class Region
    {
         public Region()
        {
            this.SabaqGroups = new HashSet<SabaqGroup>();
            this.Users = new HashSet<User>();
        }

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Region Id")]
        public int RegionId { get; set; }

        public short RegionTypeId { get; set; }       

        [Required]
        [Display(Name = "Region Name")]
        public string RegionName { get; set; }

        [Display(Name = "Parent Region Id")]
        public Nullable<int> ParentRegionId { get; set; }

        [ForeignKey("ParentRegionId")]        
        public Region Region1 { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [ForeignKey("RegionTypeId")]
        public RegionType RegionType { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<SabaqGroup> SabaqGroups { get; set; }

        public string RegionNameShow
        {
            get
            {
                if (Region1 != null)
                    return Region1.RegionName + "_" + RegionName;

                return RegionName;
            }
        }

    }
}