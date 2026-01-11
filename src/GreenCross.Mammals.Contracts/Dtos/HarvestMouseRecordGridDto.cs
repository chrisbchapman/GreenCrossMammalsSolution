namespace GreenCross.Mammals.Contracts.Dtos;

public class HarvestMouseRecordGridDto
{
    public int RecordId { get; set; }
    public DateOnly DateRecorded { get; set; }
    public string RecorderInitials { get; set; } = string.Empty;
    public string GridReference { get; set; } = string.Empty;
    public string LocationName { get; set; } = string.Empty;
    public string VerificationStatus { get; set; } = string.Empty;
}
