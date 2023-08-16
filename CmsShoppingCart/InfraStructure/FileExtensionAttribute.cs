using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace CmsShoppingCart.InfraStructure
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile; 
            
            if (file != null) { 
            
            var extension = Path.GetExtension(file.FileName);

                string[] extensions = { "jpg", "png" };
                bool result = extensions.Any(x => extension.EndsWith(x));
                
                if (!result) {
                    return new ValidationResult(GetErrorMessage());
                }
                
            }
            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "Allowed extensions are jbg and png.";
        }
    }
}
