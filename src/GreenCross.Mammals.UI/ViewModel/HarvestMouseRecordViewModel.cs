using GreenCross.Utils.WinForm.Attributes;

namespace GreenCross.Mammals.UI.ViewModels;

public class HarvestMouseRecordViewModel
{
    [DataGridColumn(60, 60, "Record ID", true, 1)]
    public int RecordId { get; set; }

    [DataGridColumn(100, 80, "Date", true, 2)]
    public string Date { get; set; } = string.Empty;

    [DataGridColumn(80, 60, "Recorder", true, 3)]
    public string RecorderInitials { get; set; } = string.Empty;

    [DataGridColumn(120, 80, "Grid Ref", true, 4)]
    public string GridReference { get; set; } = string.Empty;

    [DataGridColumn(100, 80, "Location", true, 5)]
    public string LocationName { get; set; } = string.Empty;

    [DataGridColumn(80, 60, "Status", true, 6)]
    public string VerificationStatus { get; set; } = string.Empty;
}
