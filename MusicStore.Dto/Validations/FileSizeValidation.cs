using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Dto.Validations
{
    public class FileSizeValidation : ValidationAttribute
    {
        private readonly int maxSizeInMegabytes;

        public FileSizeValidation(int MaxSizeInMegabytes)
        {
            maxSizeInMegabytes = MaxSizeInMegabytes;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;

            if(formFile is null)
                return ValidationResult.Success;

            if (formFile.Length > maxSizeInMegabytes * 1024 * 1024)
                return new ValidationResult($"El peso del archivo no debe ser mayor a {maxSizeInMegabytes} Mb.");

            return ValidationResult.Success;
        }
    }
}
