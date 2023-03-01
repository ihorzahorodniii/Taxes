namespace Taxes.Entities
{
    public class Tax
    {
        public Guid Id { get; set; }
        public DateTime ValidFrom { get; set; }
        public double Value { get; set; }
        public Guid MunicipalityId { get; set; }
        public Guid TaxTypeId { get; set; }
    }
}
