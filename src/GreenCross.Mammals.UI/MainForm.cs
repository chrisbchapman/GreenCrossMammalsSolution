using GreenCross.BigBirdWatch.UI;
using GreenCross.Mammals.Entities.Enums;

namespace GreenCross.Mammals.UI;

public partial class MainForm : Form
{
    private readonly MammalCsvImporterFactory _factory;

    public MainForm(MammalCsvImporterFactory factory)
    {
        InitializeComponent();
        _factory = factory;

    }

    private void ImportRecordersToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var importForm = new GenericCsvImportForm<ImportDataType>(_factory, ImportDataType.Recorders);
        importForm.ShowDialog();
    }

    private void ImportRecordVeriicationStatusesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var importForm = new GenericCsvImportForm<ImportDataType>(_factory, ImportDataType.RecordVerificationStatuses);
        importForm.ShowDialog();
    }

    private void ImportLocationsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var importForm = new GenericCsvImportForm<ImportDataType>(_factory, ImportDataType.Locations);
        importForm.ShowDialog();
    }

    private void importHarvestMouiseRecordsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var importForm = new GenericCsvImportForm<ImportDataType>(_factory, ImportDataType.HarvestMouseRecords);
        importForm.ShowDialog();
    }
}
