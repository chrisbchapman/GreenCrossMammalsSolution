namespace GreenCross.Mammals.Entities;

public class Recorder : IEntity<int>
{
    public int Id
    {
        get => RecorderId;
        set => RecorderId = value;
    }

    public required int RecorderId { get; set; }
    public required string Name { get; set; }
    public required string Initials { get; set; }
    public bool WildlifeGroupMember { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}
