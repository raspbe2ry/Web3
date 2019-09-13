using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web3.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public decimal Price { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }

        [MaxLength(10)]
        public string CategoryCode { get; set; }

        public int? CatalogId { get; set; }
        [ForeignKey("CatalogId")]
        public virtual Catalog Catalog { get; set; }

        public virtual IList<SubOrder> SubOrders { get; set; }

        public Item()
        {
            SubOrders = new List<SubOrder>();
        }
    }
}