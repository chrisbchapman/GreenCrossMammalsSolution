using CsvHelper.Configuration;
using GreenCross.Mammals.Contracts.Dtos;

namespace GreenCross.Mammals.Data.Mappings;

public class LocationCsvMap : ClassMap<LocationDto>
{
    public LocationCsvMap()
    {
        Map(m => m.GridReference).Name("Grid Ref");
    }
}
