using GreenCross.Mammals.Data;
using GreenCross.Mammals.Entities.Enums;
using GreenCross.Utils.TextFiles.Csv;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class MammalCsvImporterFactory : CsvImporterFactory<ImportDataType>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="CsvImporterFactory{TDataType}"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="configuration">The configuration.</param>
    public MammalCsvImporterFactory(IServiceProvider serviceProvider, IConfiguration configuration)
            : base(serviceProvider, configuration)
    {
    }

    public override ICsvImporter? GetImporter(ImportDataType dataType)
    {
        return dataType switch
        {
            ImportDataType.Recorders => ServiceProvider.GetService<RecordersCsvImporterFactory>(),
            ImportDataType.HarvestMouseRecords => ServiceProvider.GetService<HarvestMouseRecordCsvImporterFactory>(),
            ImportDataType.Locations => ServiceProvider.GetService<LocationsCsvImporterFactory>(),
            ImportDataType.RecordVerificationStatuses => ServiceProvider.GetService<RecordVerificationStatusCsvImporterFactory>(),
            _ => null
        };
    }

    public override string? GetCsvFilePath(ImportDataType dataType)
    {
        string baseFolder = Configuration["CsvImportSettings:BaseDataFolder"] ?? string.Empty;
        var fileName = dataType switch
        {
            ImportDataType.Recorders => Configuration["CsvImportSettings:RecordersFile"],
            ImportDataType.HarvestMouseRecords => Configuration["CsvImportSettings:HarvestMouseRecordsFile"],
            ImportDataType.Locations => Configuration["CsvImportSettings:HarvestMouseRecordsFile"],
            ImportDataType.RecordVerificationStatuses => Configuration["CsvImportSettings:HarvestMouseRecordsFile"],
            _ => null
        };

        return fileName != null ? BuildPath(baseFolder, fileName) : null;
    }
}
