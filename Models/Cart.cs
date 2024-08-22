using System;
using System.Collections.Generic;
using System.Linq;

namespace e_Medicine.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Qty { get; set; }
        public decimal TotPrice { get; set; }
        public int MedicineId { get; set; }

    }
}
