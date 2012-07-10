using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SQMS.Web.Models
{
    public class SabaqRequest
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public long SabaqRequestId { get; set; }
        public short RequestTypeId { get; set; }
        public Nullable<long> ParentRequestId { get; set; }
        public Nullable<long> RefRequestId { get; set; }
        public System.DateTime Date { get; set; }
        public string Status { get; set; }
        public System.DateTime RequestDate { get; set; }
        public long RequestedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }

        public virtual RequestType RequestType { get; set; }
    }
}