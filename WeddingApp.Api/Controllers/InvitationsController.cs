using Microsoft.AspNetCore.Mvc;
using WeddingApp.Api.Services;
using WeddingApp.Shared.DTOs;

namespace WeddingApp.Api.Controllers;

[ApiController]
[Route("api/invitations")]
public class InvitationController : ControllerBase
{
    private readonly ILogger<InvitationController> _logger;
    private readonly IInvitationService _invitationService;

    public InvitationController(ILogger<InvitationController> logger, IInvitationService invitationService)
    {
        _logger = logger;
        _invitationService = invitationService;
    }
    
    [HttpGet ("{token}")]
    public async Task<ActionResult<InvitationDto>> GetInvitation(string token)
    {
        var invitation = await _invitationService.GetByTokenAsync(token);

        if (invitation == null)
        {
            _logger.LogInformation($"Invitation with Token {token} not found");
            return NotFound();
        }

        return Ok(invitation);
    }
}