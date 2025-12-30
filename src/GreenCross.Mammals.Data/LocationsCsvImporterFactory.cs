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

    protected override async Task<ImportResult> ProcessRecordsAsync(List<LocationDto> records, ImportResult result)
    {
        var uniqueRecords = records
            .GroupBy(l => l.GridReference)
            .Select(g => g.First())
            .ToList();

        foreach (var record in uniqueRecords)
        {
            try
            {
                var location = new Location
                {
                    Id = 0, // Will be set by IDENTITY_INSERT
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
        }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Locations ON");
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Locations OFF");
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
