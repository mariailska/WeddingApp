using Microsoft.AspNetCore.Mvc;
using WeddingApp.Api.Services;
using WeddingApp.Shared.DTOs;

namespace WeddingApp.Api.Controllers;

[ApiController]
[Route("api/admin/invitations")]
public class AdminInvitationsController : ControllerBase
{
    private readonly ILogger<AdminInvitationsController> _logger;
    private readonly IInvitationService _invitationService;

    public AdminInvitationsController(ILogger<AdminInvitationsController> logger, IInvitationService invitationService)
    {
        _logger = logger;
        _invitationService = invitationService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvitation([FromBody] CreateInvitationDto createInvitationDto)
    {
        
        var createdInvitation = await _invitationService.CreateAsync(createInvitationDto);
        
        return Created($"/api/invitations/{createdInvitation.Token}", createdInvitation);
    }

    [HttpPut("{token}")]
    public async Task<IActionResult> UpdateInvitation(string token, [FromBody] UpdateInvitationDto updateInvitationDto)
    {
        var updatedInvitation = await _invitationService.UpdateAsync(token, updateInvitationDto);
    
        if (updatedInvitation == null)
        {
            _logger.LogWarning($"Attempted to update non-existent invitation with Token: {token}");
            return NotFound();
        }

        return Ok(updatedInvitation);
    }
}