using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Npgsql;
using WeddingApp.Api.Data;
using WeddingApp.Api.Entities;
using WeddingApp.Shared.DTO_s;
using WeddingApp.Shared.DTOs;

namespace WeddingApp.Api.Services;

public class InvitationService :  IInvitationService
{
    private readonly WeddingDbContext _context;
    private readonly ILogger<InvitationService> _logger;
    private readonly IEmailService _emailnotify;

    public InvitationService(WeddingDbContext context, ILogger<InvitationService> logger, IEmailService emailnotify)
    {
        _context = context;
        _logger = logger;
        _emailnotify = emailnotify;
    }

    public async Task<InvitationDto?> GetByTokenAsync(string token)
    {
        var invitation = await _context.Invitations
            .Include(i => i.Guests)
            .FirstOrDefaultAsync(j => j.Token == token);
        if (invitation == null)
        {
            return null;
        }

        return new InvitationDto
        {
            Token = invitation.Token,
            DisplayName = invitation.DisplayName,
            AccommodationDetails = invitation.AccommodationDetails,
            Notes = invitation.Notes,
            Guests = invitation.Guests.Select(g => new GuestDto
            {
                InvitationId = g.InvitationId,
                Name = g.Name,
                Attendance = g.Attendance,
                Diet = g.Diet,
            }).ToList()
        };
    }

    public async Task<InvitationDto> CreateAsync(CreateInvitationDto createInvitationDto)
    {
        var creatingInvitation = new Invitation
        {
            Token = Guid.NewGuid().ToString(),
            DisplayName = createInvitationDto.DisplayName,
            AccommodationDetails = createInvitationDto.AccommodationDetails,
            Notes = createInvitationDto.Notes,
            Guests = createInvitationDto.Guests.Select(g => new Guest
            {
                Name = g.Name,
                Attendance = g.Attendance,
                Diet = g.Diet
            }).ToList()
        };
        
        _context.Invitations.Add(creatingInvitation);
        await _context.SaveChangesAsync();

        return new InvitationDto
        {
            Token = creatingInvitation.Token,
            DisplayName = creatingInvitation.DisplayName,
            AccommodationDetails = creatingInvitation.AccommodationDetails,
            Notes = creatingInvitation.Notes,
            Guests = creatingInvitation.Guests.Select(g => new GuestDto
            {
                InvitationId = g.InvitationId,
                Name = g.Name,
                Attendance = g.Attendance,
                Diet = g.Diet
            }).ToList()
        };
    }

    public async Task<InvitationDto?> UpdateAsync(string token, UpdateInvitationDto updateInvitationDto)
    {
        var updatingInvitation = await _context.Invitations
            .Include(i => i.Guests)
            .FirstOrDefaultAsync(j => j.Token == token);
        
        if (updatingInvitation == null)
        {
            return null;
        }

        updatingInvitation.DisplayName = updateInvitationDto.DisplayName;
        updatingInvitation.AccommodationDetails = updateInvitationDto.AccommodationDetails;
        updatingInvitation.Notes = updateInvitationDto.Notes;
        
        _context.Guests.RemoveRange(updatingInvitation.Guests);
        updatingInvitation.Guests = updateInvitationDto.Guests.Select(g => new Guest
        {
            Name = g.Name,
            Attendance = g.Attendance,
            Diet = g.Diet
        }).ToList();
        
        await _context.SaveChangesAsync();
        
        var jsonOptions = new JsonSerializerOptions 
        { 
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) 
        };
        jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        var jsonString = JsonSerializer.Serialize(updateInvitationDto, jsonOptions);
        
        var messageBody = $@"
Hej Maria! 

Gość właśnie zaktualizował swoje zaproszenie.
Token: {token}

Zapisane dane:
{jsonString}
";
        await _emailnotify.SendNotificationAsync(
            subject: $"Aktualizacja zaproszenia: {updateInvitationDto.DisplayName}", 
            message: messageBody);
        
        return new InvitationDto
        {
            Token = updatingInvitation.Token,
            DisplayName = updatingInvitation.DisplayName,
            AccommodationDetails = updatingInvitation.AccommodationDetails,
            Notes = updatingInvitation.Notes,
            Guests = updatingInvitation.Guests.Select(g => new GuestDto
            {
                InvitationId = g.InvitationId,
                Name = g.Name,
                Attendance = g.Attendance,
                Diet = g.Diet
            }).ToList()
        };
    }
    
    public async Task<bool> DeleteAsync(string token)
    {
        var invitation = await _context.Invitations
            .FirstOrDefaultAsync(i => i.Token == token);
        
        if (invitation == null)
        {
            return false;
        }
        
        _context.Invitations.Remove(invitation);
        await _context.SaveChangesAsync();

        return true;
    }
}