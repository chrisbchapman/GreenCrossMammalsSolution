using CsvHelper.Configuration;
using GreenCross.Mammals.Contracts.Dtos;

namespace GreenCross.Mammals.Data.Mappings;

public class RecorderCsvMap : ClassMap<RecorderDto>
{
    public RecorderCsvMap()
    {
        Map(m => m.Name).Name("Name");
        Map(m => m.Initials).Name("Initials");
        Map(m => m.Member).Name("Member");
    }
}
