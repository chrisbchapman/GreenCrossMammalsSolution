using CsvHelper.Configuration;
using GreenCross.Mammals.Contracts.Dtos;

namespace GreenCross.Mammals.Data.Mappings;

public class HarvestMouseNestLocationExportCsvMap : ClassMap<HarvestMouseNestLocationExportDto>
{
    public HarvestMouseNestLocationExportCsvMap()
    {
        Map(m => m.NestCount).Name("NestCount");
        Map(m => m.Latitude).Name("Latitude");
        Map(m => m.Longitude).Name("Longitude");
    }
}
