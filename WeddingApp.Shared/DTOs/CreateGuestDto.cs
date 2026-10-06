using WeddingApp.Shared.Enums;

namespace WeddingApp.Shared.DTOs;

public class CreateGuestDto
{
    public string? Name { get; set; }
    public AttendanceStatus Attendance { get; set; } =
        AttendanceStatus.Undetermined;
    public DietaryRequirement Diet { get; set; } = DietaryRequirement.Normal;

}