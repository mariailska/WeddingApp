using WeddingApp.Shared.DTO_s;

namespace WeddingApp.Shared.DTOs;

public class InvitationDto
{
    public string Token { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;
    
    public string? AccommodationDetails { get; set; }

    public string? Notes { get; set; }

    public List<GuestDto> Guests { get; set; } = new();
}