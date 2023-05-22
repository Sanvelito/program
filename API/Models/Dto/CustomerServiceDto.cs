﻿namespace API.Models.Dto
{
    public class CustomerServiceDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ServiceName { get; set; }
        public DateTime DeadLine { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}
