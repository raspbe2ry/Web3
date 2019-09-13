using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web3.Models
{
    public class Vendor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(20)]
        public string Zip { get; set; }

        [MaxLength(30)]
        public string Telephone { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Code { get; set; }

        public int? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        public virtual IList<SubOrder> SubOrders { get; set; }
        public virtual IList<Catalog> Catalogs { get; set; }

        public Vendor()
        {
            SubOrders = new List<SubOrder>();
            Catalogs = new List<Catalog>();
        }
    }
}