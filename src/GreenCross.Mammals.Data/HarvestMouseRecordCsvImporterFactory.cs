using GreenCross.Mammals.Contracts.Dtos;
using GreenCross.Mammals.Data.Mappings;
using GreenCross.Mammals.Entities;
using GreenCross.Utils.EntityFramework;
using GreenCross.Utils.TextFiles.Csv;
using Microsoft.EntityFrameworkCore;

namespace GreenCross.Mammals.Data;

public class HarvestMouseRecordCsvImporterFactory : CsvImporterBase<HarvestMouseRecordDto, HarvestMouseRecordCsvMap>
{
    private readonly MammalDbContext _context;

    public override string DataTypeName => "HarvestMouseRecords";

    public HarvestMouseRecordCsvImporterFactory(MammalDbContext context)
    {
        _context = context;
    }

    protected override async Task<ImportResult> ProcessRecordsAsync(List<HarvestMouseRecordDto> records, ImportResult result)
    {
        foreach (var record in records)
        {
            try
            {
                var recorder = new HarvestMouseRecord
                {
                    Id = 0,
                    SourceRecordId = record.RecordId,
                    DateRecorded = DateOnly.FromDateTime(record.Date),
                    Recorder = await _context.Recorders.FirstOrDefaultAsync(r => r.Initials == record.RecorderIntials)
                               ?? throw new Exception($"Recorder with initials '{record.RecorderIntials}' not found."),
                    Location = await _context.Locations.FirstOrDefaultAsync(l => l.Easting == record.GridRef)
                                 ?? throw new Exception($"Location with grid reference '{record.GridRef}' not found."),
                    VerificationStatus = await _context.RecordVerificationStatuses.FirstOrDefaultAsync(rvs => rvs.Status == record.Status)
                                         ?? throw new Exception($"Verification status '{record.Status}' not found."),

                };

                _context.HarvestMouseRecords.Add(recorder);
                result.RecordsImported++;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Error importing record '{record.RecordId}': {ex.Message}");
            }
        }

        await DbContextHelper.SaveChangesWithIdentityInsertAsync(_context, "HarvestMouseRecords");

        return result;
    }

}
