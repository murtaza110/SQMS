using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SQMS.Web.Models
{
    public class RequestType
    {
        public RequestType()
        {
            this.SabaqRequests = new HashSet<SabaqRequest>();
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public short RequestTypeId { get; set; }

        [Column("RequestType")]
        public string RequestType1 { get; set; }

        public virtual ICollection<SabaqRequest> SabaqRequests { get; set; }
    }
}