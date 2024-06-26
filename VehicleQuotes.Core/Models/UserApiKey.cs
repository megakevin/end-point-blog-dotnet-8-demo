using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace VehicleQuotes.Core.Models;

[Index(nameof(Value), IsUnique = true)]
public class UserApiKey
{
    [JsonIgnore]
    public int ID { get; set; }
    [Required]
    public required string Value { get; set; }
    [JsonIgnore]
    [Required]
    public string UserID { get; set; } = default!;

    [JsonIgnore]
    public IdentityUser User { get; set; } = default!;
}
