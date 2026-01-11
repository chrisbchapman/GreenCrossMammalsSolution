using CsvHelper.Configuration;
using GreenCross.Mammals.Contracts.Dtos;

namespace GreenCross.Mammals.Data.Mappings;

public class HarvestMouseNestMonadExportCsvMap : ClassMap<HarvestMouseNestMonadExportDto>
{
    public HarvestMouseNestMonadExportCsvMap()
    {
        Map(m => m.MonadSquare).Name("Monad Square");
        Map(m => m.NestCount).Name("Total Nest Count");
    }
}
