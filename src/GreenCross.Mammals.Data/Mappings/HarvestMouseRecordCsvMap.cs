using CsvHelper.Configuration;
using GreenCross.Mammals.Contracts.Dtos;

namespace GreenCross.Mammals.Data.Mappings;

public class HarvestMouseRecordCsvMap : ClassMap<HarvestMouseRecordDto>
{
    public HarvestMouseRecordCsvMap()
    {
        Map(m => m.RecordId).Name("RecordId");
        Map(m => m.Date).Name("Date");
        Map(m => m.RecorderIntials).Name("RecorderIntials");
        Map(m => m.GridRef).Name("GridRef");
        Map(m => m.Count).Name("Count");
        Map(m => m.Comment).Name("Comment");
        Map(m => m.Status).Name("Status");
    }

}
