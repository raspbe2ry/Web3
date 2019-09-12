﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web3.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Employee { get; set; }

        public virtual IList<OrderItem> Items { get; set; }
        public virtual IList<SubOrder> SubOrders { get; set; }

        public Order()
        {
            Items = new List<OrderItem>();
            SubOrders = new List<SubOrder>();
        }
    }
}