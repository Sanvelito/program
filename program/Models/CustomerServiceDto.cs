using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.Models
{
    public class CustomerServiceDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ServiceName { get; set; }
        public DateTime DeadLine { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}
