using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Medicine.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MedicineId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Qty { get; set; }
        public decimal TotPrice { get; set; }
    }
}
