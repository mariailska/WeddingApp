using WeddingApp.Api.Entities;
using WeddingApp.Shared.DTOs;

namespace WeddingApp.Api.Services;

public interface IInvitationService
{ 
    public Task<InvitationDto> GetByTokenAsync(string token);
    public Task<InvitationDto> CreateAsync(CreateInvitationDto createInvitationDto);
    public Task<InvitationDto?> UpdateAsync(string token, UpdateInvitationDto updateInvitationDto);
    public Task<bool> DeleteAsync(string token);
}