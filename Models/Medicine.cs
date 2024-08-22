using System;
using System.Data;

namespace e_Medicine.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Qty { get; set; }
        public DateTime DateOfExp { get; set; }
        public string ImgUrl { get; set; }
        public int Status { get; set; }
        public string  Type { get; set; }
    }

}
