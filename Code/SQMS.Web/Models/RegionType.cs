using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SQMS.Web.Models
{
    public class RegionType
    {
        public RegionType()
        {
            this.Regions = new HashSet<Region>();
        }

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Region Type Id")]
        public short RegionTypeId { get; set; }

        [Required]
        [Display(Name = "Region Type")]
        [Column("RegionType")]
        public string RegionTypeName { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}