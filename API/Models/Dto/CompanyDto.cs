using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Models.Dto
{
    public class CompanyDto
    {
        public string CompanyName { get; set; }
        //public List<CompanyServiceDto> Services { get; set; } = new List<CompanyServiceDto>();
        public Dictionary<string, List<CompanyServiceDto>> ServicesGroup { get; set; } = new();
    }
}
