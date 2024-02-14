using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Dto.Validations
{
    public class FileTypeValidation : ValidationAttribute
    {
        private readonly string[] validTypes;
        

        public FileTypeValidation(string[] validTypes)
        {
            this.validTypes = validTypes;
        }

        public FileTypeValidation(FileTypeGroup fileTypeGroup)
        {
            if(fileTypeGroup is FileTypeGroup.Image)
            {
                validTypes = ["image/jpeg", "image/png", "image/jpg"];
            }
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;

            if(formFile is null)
                return ValidationResult.Success;

            if (!validTypes.Contains(formFile.ContentType))
                return new ValidationResult($"El tipo de archivo no es válido, debe ser: {string.Join(",", validTypes)}");

            return ValidationResult.Success;
        }

    }

    public enum FileTypeGroup
    {
        Image
    }
}
