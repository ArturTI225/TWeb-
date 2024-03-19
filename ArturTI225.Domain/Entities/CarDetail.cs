namespace ArturTI225.Domain.Entities
{
    public class CarDetail
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string EngineType { get; set; }
        public string Transmission { get; set; }
        public bool HasNavigation { get; set; }
        public bool HasSunroof { get; set; }
        public bool HasLeatherSeats { get; set; }
    }
}