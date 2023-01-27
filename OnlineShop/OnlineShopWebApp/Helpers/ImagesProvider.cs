using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using OnlineShopWebApp.Interfaces;

namespace OnlineShopWebApp.Helpers
{
	public class ImagesProvider : IImagesProvider
	{
		private readonly IWebHostEnvironment _appEnvironment;

		public ImagesProvider(IWebHostEnvironment appEnvironment)
		{
			_appEnvironment = appEnvironment;
		}

		public List<string> SafeFiles(IFormFile[] files, ImageFolders folder)
		{
			var imagesPaths = new List<string>();
			foreach (var file in files)
			{
				var imagePath = SafeFile(file, folder);
				imagesPaths.Add(imagePath);
			}
			return imagesPaths;
		}

		public string SafeFile(IFormFile file, ImageFolders folder)
		{
			if (file != null)
			{
				var imagePath = Path.Combine(_appEnvironment.WebRootPath + "/images/" + folder);
				if (!Directory.Exists(imagePath))
				{
					Directory.CreateDirectory(imagePath);
				}

				var fileName = Guid.NewGuid() + "." + file.FileName.Split('.').Last();
				string path = Path.Combine(imagePath, fileName);
				using (var fileStream = new FileStream(path, FileMode.Create))
				{
					file.CopyTo(fileStream);
				}
				return "/images/" + folder + "/" + fileName;
			}
			return null;
		}

	}
}
