using WeddingApp.Shared.DTOs;

namespace WeddingApp.Client.Services;

public interface IInvitationService
{
    Task<InvitationDto?> GetInvitationAsync(string token);
    Task<bool> UpdateInvitationAsync(string token, UpdateInvitationDto dto);
}