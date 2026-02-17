using GreenCross.Mammals.Entities.Enums;
using GreenCross.Utils.TextFiles.Csv;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GreenCross.Mammals.Data;

public class MammalCsvExporterFactory : CsvExporterFactory<ExportDataType>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CsvImporterFactory{TDataType}"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="configuration">The configuration.</param>
    public MammalCsvExporterFactory(IServiceProvider serviceProvider, IConfiguration configuration)
            : base(serviceProvider, configuration)
    {
    }

    public override string? GetCsvFilePath(ExportDataType dataType)
    {
        string baseFolder = Configuration["CsvExportSettings:BaseDataFolder"] ?? string.Empty;
        var fileName = dataType switch
        {
            ExportDataType.HarvestMouseRecordsForQGIS1KmFile => Configuration["CsvExportSettings:HarvestMouseRecordsForQGIS1KmFile"],
            ExportDataType.HarvestMouseNestsByMonadFile => Configuration["CsvExportSettings:HarvestMouseNestsByMonadFile"],
            ExportDataType.HarvestMouseNestsByLocation => Configuration["CsvExportSettings:HarvestMouseNestsByLocationFile"],
            _ => null
        };

        return fileName != null ? BuildPath(baseFolder, fileName) : null;
    }

    public override ICsvExporter? GetExporter(ExportDataType dataType)
    {
        return dataType switch
        {
            ExportDataType.HarvestMouseRecordsForQGIS1KmFile => ServiceProvider.GetService<HarvestMouseRecords1KmCsvExporter>(),
            ExportDataType.HarvestMouseNestsByMonadFile => ServiceProvider.GetService<HarvestMouseNestsMonadCsvExporter>(),
            ExportDataType.HarvestMouseNestsByLocation => ServiceProvider.GetService<HarvestMouseNestLocationCsvExporter>(),
            _ => null
        };
    }
}
