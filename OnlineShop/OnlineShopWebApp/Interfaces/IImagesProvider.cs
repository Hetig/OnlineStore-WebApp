using Microsoft.AspNetCore.Http;
using OnlineShopWebApp.Helpers;
using System.Collections.Generic;

namespace OnlineShopWebApp.Interfaces
{
	public interface IImagesProvider
	{
		List<string> SafeFiles(IFormFile[] files, ImageFolders folder);
		string SafeFile(IFormFile file, ImageFolders folder);
	}
}
