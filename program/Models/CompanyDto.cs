using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.Models
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public long CompanyPhoneNumber { get; set; }
        public string CompanyEmail { get; set; }
        public byte[] CompanyImage { get; set; }
        public Dictionary<string, List<CompanyServiceDto>> ServicesGroup { get; set; } = new();
    }
}
