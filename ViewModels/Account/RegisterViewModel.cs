using System.ComponentModel.DataAnnotations;

namespace AuctionsWebsitePragmatic.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required, StringLength(19, MinimumLength = 4, ErrorMessage ="The username must be more than 3 characters and less than 20 characters")]
        public string Username { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, MinLength(8, ErrorMessage ="The password must be at least 8 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
