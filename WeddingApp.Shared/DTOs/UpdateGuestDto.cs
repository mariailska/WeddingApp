using WeddingApp.Shared.Enums;

namespace WeddingApp.Shared.DTOs;

public class UpdateGuestDto
{
    public int? Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public AttendanceStatus Attendance { get; set; }
    public DietaryRequirement Diet { get; set; }
}