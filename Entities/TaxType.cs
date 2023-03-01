namespace Taxes.Entities
{
    public class TaxType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public short Priority { get; set; }
        public List<Tax> Tax { get; set; }
    }
}
