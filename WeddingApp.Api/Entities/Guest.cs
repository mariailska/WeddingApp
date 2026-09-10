using WeddingApp.Api.Entities;
using WeddingApp.Shared.Enums;

namespace WeddingApp.Api;

public class Guest
{
    public int Id { get; set; }
    
    public int InvitationId { get; set; }
    
    public Invitation Invitation { get; set; } = null!;
    
    public string Name { get; set; } = string.Empty;

    public AttendanceStatus Attendance { get; set; } =
        AttendanceStatus.Undetermined;

    public DietaryRequirement Diet { get; set; } = DietaryRequirement.Normal;
}