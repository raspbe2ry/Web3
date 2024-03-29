﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web3.Models
{
    public class SubOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? ExpectedShipmentDate { get; set; }
        public decimal? Price { get; set; }

        public int? VendorId { get; set; }
        [ForeignKey("VendorId")]
        public virtual Vendor Vendor { get; set; }

        public int? OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public virtual IList<OrderItem> Items { get; set; }
        public virtual IList<Shipment> Shipments { get; set; }

        public SubOrder()
        {
            Items = new List<OrderItem>();
            Shipments = new List<Shipment>();
        }
    }
}