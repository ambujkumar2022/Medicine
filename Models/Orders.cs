using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace e_Medicine.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string  OrderNo { get; set; }
        public decimal OrderTot { get; set; }
        public string  OrderStatus { get; set; }
    }
}
