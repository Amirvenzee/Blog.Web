﻿using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Blog_CoreLayer.Utilities
{
    public class ImageValidation
    {
        public static bool Validate(string imageName)
        {
            var extension = Path.GetExtension(imageName);
            if (extension == null)
                return false;

            return extension.ToLower() == ".png" || extension.ToLower() == ".jpg";
        }

        public static bool Validate(IFormFile file)
        {
            try
            {
                using var image = System.Drawing.Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}