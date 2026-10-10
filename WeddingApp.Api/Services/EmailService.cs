using System.Net;
using System.Net.Mail;

namespace WeddingApp.Api.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }
    
    public async Task SendNotificationAsync(string subject, string message)
    {
        try
        {
            var server = _configuration["EmailSettings:Server"];
            var port = int.Parse(_configuration["EmailSettings:Port"] ?? "587");
            var senderName = _configuration["EmailSettings:SenderName"];
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var username = _configuration["EmailSettings:Username"];
            var password = _configuration["EmailSettings:Password"];
            var recipientEmail = _configuration["EmailSettings:RecipientEmail"];
            var enableSsl = bool.Parse(_configuration["EmailSettings:EnableSSL"] ?? "true");

            using var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(senderEmail!, senderName);
            
            mailMessage.To.Add(senderEmail!); 
            mailMessage.To.Add(recipientEmail!);
            
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = false;

            using var smtpClient = new SmtpClient(server, port);
            smtpClient.Credentials = new NetworkCredential(username, password);
            smtpClient.EnableSsl = enableSsl;

            await smtpClient.SendMailAsync(mailMessage);
            
            _logger.LogInformation("Email notification sent successfully. Topic: {Subject}", subject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send the email notification. Topic: {Subject}", subject);
        }
    }
}