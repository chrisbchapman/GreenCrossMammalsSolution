using GreenCross.Mammals.Contracts.Dtos;
using GreenCross.Mammals.Contracts.Repositories;
using GreenCross.Mammals.Data.Mappings;
using GreenCross.Utils.TextFiles.Csv;

namespace GreenCross.Mammals.Data;

public class HarvestMouseNestLocationCsvExporter : CsvExporterBase<HarvestMouseNestLocationExportDto, HarvestMouseNestLocationExportCsvMap>
{
    private readonly IHarvestMouseRecordRepository _repository;

    public override string DataTypeName => "HarvestMouseNestsMonad";

    public HarvestMouseNestLocationCsvExporter(IHarvestMouseRecordRepository repository)
    {
        _repository = repository;
    }

    protected override async Task<ExportResult<HarvestMouseNestLocationExportDto>> GetDataToExportAsync(
        ExportResult<HarvestMouseNestLocationExportDto> result,
        IProgress<int>? progress = null,
        CancellationToken cancellationToken = default)
    {
        var data = await _repository.GetNestLocationAsync(cancellationToken);
        result.Data = data.ToList();
        result.RecordsExported = result.Data.Count;
        return result;
    }
}
