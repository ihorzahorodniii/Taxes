using Taxes.Entities;
using Taxes.Data;
using Microsoft.EntityFrameworkCore;

namespace Taxes.Services
{
    public class TaxService : ITaxService
    {
        private readonly DBDataContext _dbContext;
        private readonly ILogger _logger;

        public TaxService(DBDataContext dbContext, ILoggerFactory logger)
        {
            _dbContext = dbContext;
            _logger = logger.CreateLogger("Log");
        }
        public async Task<List<TaxType>> GetTaxType()
        {
            return await (from taxType in _dbContext.TaxType
                          orderby taxType.Priority
                          select taxType).ToListAsync();
        }

        public async Task<Tax?> GetTax(string municipalityName, DateTime dateTax)
        {
            try
            {
                return await (from taxType in _dbContext.TaxType
                              join tax in _dbContext.Tax on taxType.Id equals tax.TaxTypeId
                              where tax.MunicipalityId == (from municipality in _dbContext.Municipality
                                                           where municipality.Name == municipalityName
                                                           select municipality.Id).First()
                              where (dateTax == tax.ValidFrom) && (taxType.Name == "daily") ||
                                                      (dateTax >= tax.ValidFrom && dateTax <= tax.ValidFrom.AddMonths(1)) && (taxType.Name == "montly") ||
                                                      (dateTax >= tax.ValidFrom && dateTax <= tax.ValidFrom.AddYears(1)) && (taxType.Name == "yearly")
                              orderby taxType.Priority descending
                              select tax).FirstAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return null;
            }
        }

        public async Task<bool> AddTax(Tax tax)
        {
            try
            {
                await _dbContext.Tax.AddAsync(new Tax
                {
                    Id = tax.Id,
                    MunicipalityId = tax.MunicipalityId,
                    TaxTypeId = tax.TaxTypeId,
                    ValidFrom = tax.ValidFrom,
                    Value = tax.Value
                });
                await _dbContext.SaveChangesAsync();
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
