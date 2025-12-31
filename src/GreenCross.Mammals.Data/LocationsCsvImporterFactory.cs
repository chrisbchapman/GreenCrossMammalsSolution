using GreenCross.Mammals.Contracts.Dtos;
using GreenCross.Mammals.Data.Mappings;
using GreenCross.Mammals.Entities;
using GreenCross.Utils.TextFiles.Csv;
using Microsoft.EntityFrameworkCore;

namespace GreenCross.Mammals.Data;

public class LocationsCsvImporterFactory : CsvImporterBase<LocationDto, LocationCsvMap>
{
    private readonly MammalDbContext _context;

    public override string DataTypeName => "Locations";

    public LocationsCsvImporterFactory(MammalDbContext context)
    {
        _context = context;
    }

    protected override async Task<ImportResult> ProcessRecordsAsync(List<LocationDto> records, ImportResult result, IProgress<int>? progress = null)
    {
        var uniqueRecords = records
            .GroupBy(l => l.GridReference)
            .Select(g => g.First())
            .ToList();

        await ProcessRecordsWithProgressAsync(uniqueRecords, async (record, index) =>
        {
            try
            {
                var location = new Location
                {
                    LocationId = 0, // Will be set by IDENTITY_INSERT
                    Easting = record.GridReference.Split(' ')[0],
                    Northing = record.GridReference.Split(' ')[1],
                };

                _context.Locations.Add(location);
                result.RecordsImported++;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Error importing record '{record.GridReference}': {ex.Message}");
            }
        }, progress);

        await _context.SaveChangesAsync();
        return result;
    }
}
