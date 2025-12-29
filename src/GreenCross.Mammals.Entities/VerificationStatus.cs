namespace GreenCross.Mammals.Entities;

public class VerificationStatus : IEntity<int>
{
    public int VerificationStatusId { get; set; }
    public string Status { get; set; } = string.Empty;

    public int Id { get => VerificationStatusId; set => VerificationStatusId = value; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}
