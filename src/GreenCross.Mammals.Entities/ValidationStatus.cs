namespace GreenCross.Mammals.Entities;

public class ValidationStatus : IEntity<int>
{
    public int Id
    {
        get => ValidationStatusId;
        set => ValidationStatusId = value;
    }

    public int ValidationStatusId { get; set; }
    // Add your entity-specific properties here
    // Example: public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}
