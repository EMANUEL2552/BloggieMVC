namespace Bloggie.WEB.Repositories
{
	public interface IImageRepository
	{
		Task<String> UploadAsync(IFormFile file);
	}
}
