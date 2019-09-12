using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web3.Models
{
    public class OrderItemShipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Note { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Employee { get; set; }

        public int OrderItemId { get; set; }
        [ForeignKey("OrderItemId")]
        public virtual OrderItem OrderItem { get; set; }

        public int ShipmentId { get; set; }
        [ForeignKey("ShipmentId")]
        public virtual Shipment Shipment { get; set; }
    }
}