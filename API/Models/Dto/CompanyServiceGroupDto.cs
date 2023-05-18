namespace API.Models.Dto
{
    public class CompanyServiceGroupDto
    {
        public string Name { get; set; }
        public List<CompanyServiceDto> Services { get; set; } = new List<CompanyServiceDto>();
    }
}
