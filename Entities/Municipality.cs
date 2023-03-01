namespace Taxes.Entities
{
    public class Municipality
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Tax> Tax { get; set; }
    }
}
