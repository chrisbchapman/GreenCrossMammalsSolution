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

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT RecordVerificationStatuses ON");
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT RecordVerificationStatuses OFF");
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        return result;
    }
}
