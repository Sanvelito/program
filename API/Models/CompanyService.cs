namespace API.Models
{
    public class CompanyService
    {
        public int Id { get; set; }
        public Company Company { get; set; }
        public Service Service { get; set; }
        public int CompanyId { get; set; }
        public int ServiceId { get; set; }
        public double Price { get; set; }

    }
}
