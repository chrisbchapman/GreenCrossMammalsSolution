using CsvHelper.Configuration;
using GreenCross.Mammals.Contracts.Dtos;

namespace GreenCross.Mammals.Data.Mappings;

public class HarvestMouseRecordCsvMap : ClassMap<HarvestMouseRecordDto>
{
    public HarvestMouseRecordCsvMap()
    {
        Map(m => m.RecordId).Name("RecordId");
        Map(m => m.Date).Name("Date").TypeConverterOption.Format("dd/MM/yyyy");
        Map(m => m.RecorderIntials).Name("Recorder");
        Map(m => m.GridRef).Name("Grid Ref");
        Map(m => m.Count).Name("Count");
        Map(m => m.Comment).Name("Comment");
        Map(m => m.Status).Name("Status");
    }
}
