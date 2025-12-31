using GreenCross.Mammals.Contracts.Dtos;
using GreenCross.Mammals.Data.Mappings;
using GreenCross.Mammals.Entities;
using GreenCross.Utils.TextFiles.Csv;
using Microsoft.EntityFrameworkCore;

namespace GreenCross.Mammals.Data;

public class RecordVerificationStatusCsvImporterFactory : CsvImporterBase<RecordVerificationStatusDto, RecordVerificationStatusCsvMap>
{
    private readonly MammalDbContext _context;

    public RecordVerificationStatusCsvImporterFactory(MammalDbContext context)
    {
        _context = context;
    }

    public override string DataTypeName => "Record Verification Statuses";
    protected override async Task<ImportResult> ProcessRecordsAsync(List<RecordVerificationStatusDto> records, ImportResult result, IProgress<int>? progress = null)
    {
        var uniqueRecords = records
            .GroupBy(rvs => rvs.Status)
            .Select(g => g.First())
            .ToList();

        await ProcessRecordsWithProgressAsync(uniqueRecords, async (record, index) =>
        {
            try
            {
                var status = new RecordVerificationStatus
                {
                    RecordVerificationStatusId = 0, // Will be set by IDENTITY_INSERT
                    Status = record.Status
                };

                _context.RecordVerificationStatuses.Add(status);
                result.RecordsImported++;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Error importing record '{record.Status}': {ex.Message}");
            }
        }, progress);

        await _context.SaveChangesAsync();
        return result;
    }
}
