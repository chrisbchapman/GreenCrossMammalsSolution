using GreenCross.Mammals.Contracts.Dtos;
using GreenCross.Mammals.Contracts.Repositories;
using GreenCross.Mammals.Data.Mappings;
using GreenCross.Utils.TextFiles.Csv;

namespace GreenCross.Mammals.Data;

public class HarvestMouseNestsMonadCsvExporter : CsvExporterBase<HarvestMouseNestMonadExportDto, HarvestMouseNestMonadExportCsvMap>
{
    private readonly IHarvestMouseRecordRepository _repository;

    public override string DataTypeName => "HarvestMouseNestsMonad";

    public HarvestMouseNestsMonadCsvExporter(IHarvestMouseRecordRepository repository)
    {
        _repository = repository;
    }

    protected override async Task<ExportResult<HarvestMouseNestMonadExportDto>> GetDataToExportAsync(
        ExportResult<HarvestMouseNestMonadExportDto> result,
        IProgress<int>? progress = null,
        CancellationToken cancellationToken = default)
    {
        var data = await _repository.GetNestCountByMonadAsync(cancellationToken);
        result.Data = data.ToList();
        result.RecordsExported = result.Data.Count;
        return result;
    }
}
