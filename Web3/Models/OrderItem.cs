using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web3.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public decimal Qty { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public decimal CatalogDiscount { get; set; }
        public decimal ExtraDiscount { get; set; }

        [MaxLength(200)]
        public string ExtraDiscountDescription { get; set; }

        public int? SubOrderId { get; set; }
        [ForeignKey("SubOrderId")]
        public virtual SubOrder SubOrder { get; set; }

        public int? ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }

        public IList<Shipment> Shipments { get; set; }

        public OrderItem()
        {
            Shipments = new List<Shipment>();
        }
    }
}