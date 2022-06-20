namespace MarkaKod.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public double Lat { get; set; }
        public string? LatString { get; set; }
        public double Long { get; set; }
        public string? LongString { get; set; }
        public bool CurrentLocation { get; set; } = false;
        public bool Visited { get; set; } = false;
        public double Km { get; set; }

    }
}
