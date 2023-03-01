using Taxes.Entities;

namespace Taxes.Services
{
    public interface ITaxService
    {
        public Task<bool> AddTax(Tax tax);

        public Task<Tax> GetTax(string municipalityName, DateTime dateTax);

        public Task<List<TaxType>> GetTaxType();

    }
}
