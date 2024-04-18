using System.ComponentModel.DataAnnotations;

namespace VehicleQuotes.WebApi.ResourceModels
{
    public class User
    {
        [Required]
        public required string UserName { get; set; }
        [Required]
        public string Password { get; set; } = default!;
        [Required]
        public required string Email { get; set; }
    }
}