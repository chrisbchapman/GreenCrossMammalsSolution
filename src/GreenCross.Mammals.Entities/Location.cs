namespace GreenCross.Mammals.Entities
{
    public class Location : IEntity<int>
    {
        public int LocationId { get; set; }
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;

        public string FullGridRef => $"{Latitude} {Longitude}";

        public int Id
        {
            get => LocationId;
            set => LocationId = value;
        }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
