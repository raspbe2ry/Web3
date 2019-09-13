using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web3.Models
{
    public class Catalog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public decimal Discount { get; set; }
        public DateTime BeginingDate { get; set; }
        public DateTime EndDate { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }

        public int? VendorId { get; set; }
        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; }

    }
}