using GreenCross.Mammals.Contracts.Dtos;
using GreenCross.Mammals.Contracts.Repositories;
using GreenCross.Mammals.Data.Mappings;
using GreenCross.Utils.TextFiles.Csv;

namespace GreenCross.Mammals.Data;

public class HarvestMouseRecords1KmCsvExporter : CsvExporterBase<HarvestMouseNest1KmExportDto, HarvestMouseNest1KmExportCsvMap>
{
    private readonly IHarvestMouseRecordRepository _repository;

    public override string DataTypeName => "HarvestMouseNests1Km";

    public HarvestMouseRecords1KmCsvExporter(IHarvestMouseRecordRepository repository)
    {
        _repository = repository;
    }

    protected override async Task<ExportResult<HarvestMouseNest1KmExportDto>> GetDataToExportAsync(
        ExportResult<HarvestMouseNest1KmExportDto> result,
        IProgress<int>? progress = null,
        CancellationToken cancellationToken = default)
    {
        var data = await _repository.GetNestCountBy1KmSquareAsync(cancellationToken);
        result.Data = data.ToList();
        result.RecordsExported = result.Data.Count;
        return result;
    }
}
