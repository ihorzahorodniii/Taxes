namespace Taxes.Services
{
    public interface IImportService
    {
        public Task<bool> LoadFile(IFormFile fileData);
        public Task<bool> ImportData();
    }
}
