using CsvHelper.Configuration;
using GreenCross.Mammals.Contracts.Dtos;

namespace GreenCross.Mammals.Data.Mappings;

public class HarvestMouseNest1KmExportCsvMap : ClassMap<HarvestMouseNest1KmExportDto>
{
    public HarvestMouseNest1KmExportCsvMap()
    {
        Map(m => m.MonadSquare).Name("Monad Square");
        Map(m => m.Year).Name("Year Recorded");
        Map(m => m.NestCount).Name("Nest Count");
    }
}
