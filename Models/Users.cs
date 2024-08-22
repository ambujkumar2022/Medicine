using System;

namespace e_Medicine.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PassW { get; set; }
        public string Email { get; set; }
        public decimal Fund { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
        public DateTime DateOfCreate { get; set; }

    }
}
