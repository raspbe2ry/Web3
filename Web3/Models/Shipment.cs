using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web3.Models
{
    public class Shipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(200)]
        public string Note { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UsetId")]
        public virtual ApplicationUser Employee { get; set; }

    }
}