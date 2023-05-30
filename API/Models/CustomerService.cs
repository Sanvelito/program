namespace API.Models
{
    public class CustomerService
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public CompanyService CompanyService { get; set; }
        public int CompanyServiceId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeadLine { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}
