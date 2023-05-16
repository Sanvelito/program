namespace API.Models
{
    public class CustomerService
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public CompanyService CompanyService { get; set; }
        public int CompanyServiceId { get; set; }
    }
}
