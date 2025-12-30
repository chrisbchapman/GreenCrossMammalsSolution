using CsvHelper.Configuration;
using GreenCross.Mammals.Contracts.Dtos;

namespace GreenCross.Mammals.Data.Mappings;

public class RecordVerificationStatusCsvMap : ClassMap<RecordVerificationStatusDto>
{
    public RecordVerificationStatusCsvMap()
    {
        Map(rvs => rvs.Status).Name("Status");
    }

}
