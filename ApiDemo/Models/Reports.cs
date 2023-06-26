namespace ApiDemo.Models
{
    public class Reports
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Lga { get; set; }
        public string Lcda { get; set; }
        public string? Images { get; set; }
        public DateTime TimeofEvent { get; set; }
        public string Cookie { get; set; }
        public int Ranking { get; set; }
        public DateTime DateCreated { get; set;}
        public DateTime DateUpdated { get; set; }

    }
}
