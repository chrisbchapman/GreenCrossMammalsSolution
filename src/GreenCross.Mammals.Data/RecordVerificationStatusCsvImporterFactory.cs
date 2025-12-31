using GreenCross.Mammals.Contracts.Dtos;
using GreenCross.Mammals.Data.Mappings;
using GreenCross.Mammals.Entities;
using GreenCross.Utils.EntityFramework;
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
    protected override async Task<ImportResult> ProcessRecordsAsync(List<RecordVerificationStatusDto> records, ImportResult result)
    {
        var uniqueRecords = records
            .GroupBy(rvs => rvs.Status)
            .Select(g => g.First())
            .ToList();

        foreach (var record in uniqueRecords)
        {
            try
            {
                var status = new RecordVerificationStatus
                {
                    Id = 0, // Will be set by IDENTITY_INSERT
                    Status = record.Status
                };

                _context.RecordVerificationStatuses.Add(status);
                result.RecordsImported++;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Error importing record '{record.Status}': {ex.Message}");
            }
        }

        await DbContextHelper.SaveChangesWithIdentityInsertAsync(_context, "RecordVerificationStatuses");

        return result;
    }
}
