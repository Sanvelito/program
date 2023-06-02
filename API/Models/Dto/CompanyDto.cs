using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Models.Dto
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
