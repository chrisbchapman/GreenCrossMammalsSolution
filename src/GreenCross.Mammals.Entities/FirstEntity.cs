namespace GreenCross.Mammals.Entities;

public class FirstEntity : IEntity<int>
{
    public int Id
    {
        get => FirstEntityId;
        set => FirstEntityId = value;
    }

    public required int FirstEntityId { get; set; }
    public required string FirstEntityName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}
