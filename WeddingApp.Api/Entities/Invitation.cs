using WeddingApp.Shared.DTO_s;

namespace WeddingApp.Api.Entities;

public class Invitation
{
    public int Id { get; set; }

    public string Token { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string? AccommodationDetails { get; set; }

    public string? Notes { get; set; } = string.Empty;

    public IEnumerable<Guest> Guests { get; set; } = new List<Guest>();
}