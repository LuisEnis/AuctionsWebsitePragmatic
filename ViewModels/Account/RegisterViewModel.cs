using System.ComponentModel.DataAnnotations;

namespace AuctionsWebsitePragmatic.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required, StringLength(20, MinimumLength = 3)]
        public string Username { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
