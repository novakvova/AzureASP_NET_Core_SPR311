namespace WebWorker.Interfaces;

public interface IImageService
{
    Task<string> SaveAsync(IFormFile file);
}
