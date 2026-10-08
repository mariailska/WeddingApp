using System.ComponentModel.DataAnnotations;
using WeddingApp.Shared.Enums;

namespace WeddingApp.Shared.DTOs;

public class UpdateGuestDto
{
    [Required(ErrorMessage = "Guest name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [EnumDataType(typeof(AttendanceStatus), ErrorMessage = "Invalid attendance status.")]
    public AttendanceStatus Attendance { get; set; }

    [EnumDataType(typeof(DietaryRequirement), ErrorMessage = "Invalid dietary requirement.")]
    public DietaryRequirement Diet { get; set; }
}