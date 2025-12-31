using GreenCross.Mammals.Contracts.Dtos;
using GreenCross.Mammals.Data.Mappings;
using GreenCross.Mammals.Entities;
using GreenCross.Utils.TextFiles.Csv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GreenCross.Mammals.Data;

public class HarvestMouseRecordCsvImporterFactory : CsvImporterBase<HarvestMouseRecordDto, HarvestMouseRecordCsvMap>
{
    private readonly IConfiguration _configuration;

    public override string DataTypeName => "HarvestMouseRecords";

    public HarvestMouseRecordCsvImporterFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override async Task<ImportResult> ProcessRecordsAsync(List<HarvestMouseRecordDto> records, ImportResult result, IProgress<int>? progress = null)
    {
        // Create a DbContext WITHOUT retry logic for bulk import
        var optionsBuilder = new DbContextOptionsBuilder<MammalDbContext>();
        optionsBuilder.UseSqlServer(
            _configuration.GetConnectionString("AppDb"),
            sqlOptions =>
            {
                // Do NOT enable retry for imports
                sqlOptions.CommandTimeout(300); // Longer timeout for imports
                sqlOptions.MaxBatchSize(100);
            });

        await using var importContext = new MammalDbContext(optionsBuilder.Options);

        // Cache lookups to avoid duplicate tracking
        var recorderCache = new Dictionary<string, Recorder>();
        var locationCache = new Dictionary<(string Easting, string Northing), Location>();
        var verificationStatusCache = new Dictionary<string, RecordVerificationStatus>();

        await ProcessRecordsWithProgressAsync(records, async (record, index) =>
        {
            try
            {
                // Split the grid reference into Easting and Northing components
                var gridRefParts = record.GridRef.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (gridRefParts.Length != 2)
                {
                    throw new Exception($"Invalid grid reference format '{record.GridRef}'. Expected format: 'Easting Northing'");
                }

                var easting = gridRefParts[0];
                var northing = gridRefParts[1];

                // Get or retrieve recorder
                if (!recorderCache.TryGetValue(record.RecorderIntials, out var recorder))
                {
                    recorder = await importContext.Recorders.AsNoTracking().FirstOrDefaultAsync(r => r.Initials == record.RecorderIntials)
                               ?? throw new Exception($"Recorder with initials '{record.RecorderIntials}' not found.");
                    recorderCache[record.RecorderIntials] = recorder;
                }

                // Get or retrieve location
                var locationKey = (easting, northing);
                if (!locationCache.TryGetValue(locationKey, out var location))
                {
                    location = await importContext.Locations.AsNoTracking().FirstOrDefaultAsync(l => l.Easting == easting && l.Northing == northing)
                               ?? throw new Exception($"Location with grid reference '{record.GridRef}' not found.");
                    locationCache[locationKey] = location;
                }

                // Get or retrieve verification status
                if (!verificationStatusCache.TryGetValue(record.Status, out var verificationStatus))
                {
                    verificationStatus = await importContext.RecordVerificationStatuses.AsNoTracking().FirstOrDefaultAsync(rvs => rvs.Status == record.Status)
                                         ?? throw new Exception($"Verification status '{record.Status}' not found.");
                    verificationStatusCache[record.Status] = verificationStatus;
                }

                var harvestMouseRecord = new HarvestMouseRecord
                {
                    //RecordId = 0,
                    SourceRecordId = record.RecordId,
                    DateRecorded = DateOnly.FromDateTime(record.Date),
                    RecorderId = recorder.RecorderId,
                    LocationId = location.LocationId,
                    VerificationStatusId = verificationStatus.RecordVerificationStatusId,
                };

                importContext.HarvestMouseRecords.Add(harvestMouseRecord);
                result.RecordsImported++;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Error importing record '{record.RecordId}': {ex.Message}");
            }
        }, progress);

        // Now this will work because importContext has NO retry strategy
        await importContext.SaveChangesAsync();

        return result;
    }
}
