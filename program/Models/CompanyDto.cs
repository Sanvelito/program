using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.Models
{
    public class CompanyDto
    {
        public string CompanyName { get; set; }
        public Dictionary<string, List<CompanyServiceDto>> ServicesGroup { get; set; } = new();
    }
}
