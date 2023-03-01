using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Taxes.Data;
using Taxes.Entities;

namespace Taxes.Services
{
    public struct ImportMunicipalitiesFormat
    {
        public string Municipalities { get; set; }
    }

    public class ImportService : IImportService
    {
        private readonly DBDataContext _dbContext;
        private readonly ILogger _logger;
        private string[] importData;
        private string[] sourceData;

        public ImportService(DBDataContext dbContext, ILoggerFactory logger)
        {
            _dbContext = dbContext;
            _logger = logger.CreateLogger("Log");
        }
        public async Task<bool> LoadFile(IFormFile fileData)
        {
            try
            {
                string[] dataImport;

                if (!ParseFile(fileData))
                    return false;

                importData = sourceData.Except(await (from municipality in _dbContext.Municipality
                                                      select municipality.Name).ToArrayAsync()).ToArray();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return false;
            }
        }

        public async Task<bool> ImportData()
        {
            try
            {

                List<Municipality> municipalities = new List<Municipality>();
                foreach (string municipalityName in importData)
                    municipalities.Add(new Municipality { Name = municipalityName, Id = Guid.NewGuid() });

                await _dbContext.Municipality.AddRangeAsync(municipalities);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return false;
            }
        }

        private bool ParseFile(IFormFile fileData)
        {
            try
            {
                using (var reader = new StreamReader(fileData.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    sourceData = (from m in csv.GetRecords<ImportMunicipalitiesFormat>() select m.Municipalities).ToArray();
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return false;
            }

        }
    }
}
