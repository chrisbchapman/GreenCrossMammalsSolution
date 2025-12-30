namespace GreenCross.Mammals.Entities;

public class Recorder : IEntity<int>
{
    public int Id
    {
        get => RecorderId;
        set => RecorderId = value;
    }

    public required int RecorderId { get; set; } = 0;
    public required string Name { get; set; } = string.Empty;
    public required string Initials { get; set; } = string.Empty;
    public bool WildlifeGroupMember { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; } = string.Empty;
    public string? UpdatedBy { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}
