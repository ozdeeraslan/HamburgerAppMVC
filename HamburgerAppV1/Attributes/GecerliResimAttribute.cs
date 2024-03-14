using System.ComponentModel.DataAnnotations;

namespace HamburgerAppV1.Attributes
{
    public class GecerliResimAttribute :ValidationAttribute
    {
        public double MaxDosyaBoyutuMB { get; set; } = 1;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = (IFormFile?)value;

            if (file == null)
                return ValidationResult.Success;

            if (!file.ContentType.StartsWith("image/jpeg") && !file.ContentType.StartsWith("image/png"))
            {
                return new ValidationResult("Gecersiz resim dosyasi!");
            }
            else if (file.Length > MaxDosyaBoyutuMB * 1024 * 1024)
            {
                return new ValidationResult($"Maksimum dosya boyutu : {MaxDosyaBoyutuMB} MB");
            }

            return ValidationResult.Success;
        }
    }
}
