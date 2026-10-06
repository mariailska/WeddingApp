namespace WeddingApp.Shared.DTOs;

public class UpdateInvitationDto
{
    public string Token { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? AccommodationDetails { get; set; }
    public string? Notes { get; set; }
    public List<UpdateGuestDto> Guests { get; set; } = new(); 
}