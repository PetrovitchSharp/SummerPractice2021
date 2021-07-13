using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SummerPractice2021.Helpers
{
    public class ImageHelper
    {
        public static async Task<Guid?> UploadImage(IFormFile imageData)
        {
            Guid? result = null;
            if (imageData != null)
            {
                result = Guid.NewGuid();
                var fileName = $"{result}.jpg";

                var filePath = Path.Combine("img/Uploads", fileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await imageData.CopyToAsync(fileSteam);
                }
            }
            return result;
        }

        public static string GetUrl(Guid? guid)
        {
            if (!guid.HasValue || guid.Value == Guid.Empty)
            {
                return null;
            }
            return string.Format("~/img/Uploads/{0}.jpg", guid);
        }
    }

}
