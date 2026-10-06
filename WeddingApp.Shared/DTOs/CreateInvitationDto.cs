using WeddingApp.Shared.DTO_s;

namespace WeddingApp.Shared.DTOs;

public class CreateInvitationDto
{
    public string DisplayName { get; set; } = string.Empty;
    
    public string? AccommodationDetails { get; set; }

    public string? Notes { get; set; }

    public IEnumerable<GuestDto> Guests { get; set; } = new  List<GuestDto>();

    
}