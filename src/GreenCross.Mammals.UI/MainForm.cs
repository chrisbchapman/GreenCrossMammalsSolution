using GreenCross.Mammals.Contracts.Services;
using GreenCross.Mammals.Data;
using GreenCross.Mammals.Entities.Enums;
using GreenCross.Mammals.UI.ViewModels;
using GreenCross.Utils.WinForm;
using GreenCross.Utils.WinForm.Forms;

namespace GreenCross.Mammals.UI;

public partial class MainForm : Form
{
    private readonly MammalCsvImporterFactory _importFactory;
    private readonly MammalCsvExporterFactory _exportFactory;
    private readonly IHarvestMouseRecordService _harvestMouseRecordService;

    public MainForm(MammalCsvImporterFactory importFactory, MammalCsvExporterFactory exportFactory, IHarvestMouseRecordService harvestMouseRecordService)
    {
        InitializeComponent();
        _importFactory = importFactory;
        _exportFactory = exportFactory;
        _harvestMouseRecordService = harvestMouseRecordService;
    }

    private void ImportRecordersToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var importForm = new GenericCsvImportForm<ImportDataType>(_importFactory, ImportDataType.Recorders);
        importForm.ShowDialog();
    }

    private void ImportRecordVeriicationStatusesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var importForm = new GenericCsvImportForm<ImportDataType>(_importFactory, ImportDataType.RecordVerificationStatuses);
        importForm.ShowDialog();
    }

    private void ImportLocationsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var importForm = new GenericCsvImportForm<ImportDataType>(_importFactory, ImportDataType.Locations);
        importForm.ShowDialog();
    }

    private void importHarvestMouiseRecordsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var importForm = new GenericCsvImportForm<ImportDataType>(_importFactory, ImportDataType.HarvestMouseRecords);
        importForm.ShowDialog();
    }

    private async void FullListToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var dtos = await _harvestMouseRecordService.GetRecordsForGridAsync();
        var viewModels = dtos.Select(dto => new HarvestMouseRecordViewModel
        {
            RecordId = dto.RecordId,
            Date = dto.DateRecorded.ToString("dd/MM/yyyy"),
            RecorderInitials = dto.RecorderInitials,
            GridReference = dto.GridReference,
            LocationName = dto.LocationName,
            VerificationStatus = dto.VerificationStatus
        }).ToList();

        ListRecordsForm childForm = new ListRecordsForm(viewModels, "Harvest Mouse Records");
        childForm.MdiParent = this;
        childForm.Show();
    }

    private void ForQGISToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var exportForm = new GenericCsvExportForm<ExportDataType>(_exportFactory,
            ExportDataType.HarvestMouseRecordsForQGIS1KmFile);
        exportForm.ShowDialog();
    }

    private void MonadCountToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var exportForm = new GenericCsvExportForm<ExportDataType>(_exportFactory,
            ExportDataType.HarvestMouseNestsByMonadFile);
        exportForm.ShowDialog();

    }

    private void NestLocationToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var exportForm = new GenericCsvExportForm<ExportDataType>(_exportFactory,
            ExportDataType.HarvestMouseNestsByLocation);
        exportForm.ShowDialog();
    }
}
