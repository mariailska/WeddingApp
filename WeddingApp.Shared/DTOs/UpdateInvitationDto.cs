using System.ComponentModel.DataAnnotations;

namespace WeddingApp.Shared.DTOs;

public class UpdateInvitationDto
{
    [Required(ErrorMessage = "Display name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Display name must be between 2 and 100 characters.")]
    public string DisplayName { get; set; } = string.Empty;
    
    [MaxLength(500, ErrorMessage = "Accommodation details cannot exceed 500 characters.")]
    public string? AccommodationDetails { get; set; }

    [MaxLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
    public string? Notes { get; set; }
    
    public List<UpdateGuestDto> Guests { get; set; } = new();
}