using GreenCross.Mammals.Contracts.Dtos;
using GreenCross.Mammals.Data.Mappings;
using GreenCross.Mammals.Entities;
using GreenCross.Utils.TextFiles.Csv;

namespace GreenCross.Mammals.Data;

public class RecordersCsvImporterFactory : CsvImporterBase<RecorderDto, RecorderCsvMap>
{
    private readonly MammalDbContext _context;

    public override string DataTypeName => "Recorders";

    public RecordersCsvImporterFactory(MammalDbContext context)
    {
        _context = context;
    }

    protected override async Task<ImportResult> ProcessRecordsAsync(List<RecorderDto> records, ImportResult result, IProgress<int>? progress = null)
    {
        await ProcessRecordsWithProgressAsync(records, async (record, index) =>
        {
            try
            {
                var recorder = new Recorder
                {
                    RecorderId = 0, // Will be set by IDENTITY_INSERT
                    Name = record.Name,
                    Initials = record.Initials,
                    WildlifeGroupMember = string.Equals(record.Member, "Y", StringComparison.OrdinalIgnoreCase),
                };

                _context.Recorders.Add(recorder);
                result.RecordsImported++;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Error importing record '{record.Name}': {ex.Message}");
            }
        }, progress);

        await _context.SaveChangesAsync();
        return result;
    }
}
