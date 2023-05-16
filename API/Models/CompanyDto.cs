namespace API.Models
{
    public class CompanyDto
    {
        public string CompanyName { get; set; }
        public List<CompanyServiceDto> Services { get; set; } = new List<CompanyServiceDto>();
    }
}
