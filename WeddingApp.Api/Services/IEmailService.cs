namespace WeddingApp.Api.Services;

public interface IEmailService
{
    Task SendNotificationAsync(string subject, string message);
}