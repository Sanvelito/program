namespace API.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public byte[] Image { get; set; }

        //доп инфа
    }
}
