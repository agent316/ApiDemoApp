namespace ApiDemo.Models
{
    public class AddReport
    {
        public string Description { get; set; }
        public string Address { get; set; }
        public string Lga { get; set; }
        public string Lcda { get; set; }
        public string? Images { get; set; }
        public string Cookie { get; set;}
        public long UserId { get; set; }
    }
}
