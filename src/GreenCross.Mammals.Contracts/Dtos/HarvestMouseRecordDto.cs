namespace GreenCross.Mammals.Contracts.Dtos;

public class HarvestMouseRecordDto
{
    public int RecordId { get; set; }
    public DateTime Date { get; set; }
    public string RecorderIntials { get; set; } = string.Empty;
    public string GridRef { get; set; } = string.Empty;
    public int Count { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
