using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class ContactController : Controller
{
    private readonly IConfiguration _configuration;

    public ContactController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail(string name, string email, string message)
    {
        var emailConfig = _configuration.GetSection("EmailSettings");
        string? smtpUsername = emailConfig["SmtpUsername"];
        string? smtpPassword = emailConfig["SmtpPassword"];
        string? smtpHost = emailConfig["SmtpServer"];
        int smtpPort = int.Parse(emailConfig["SmtpPort"] ?? "0");

        if (smtpUsername == null || smtpPassword == null || smtpHost == null || smtpPort == 0)
        {
            TempData["Error"] = "Configuración de correo electrónico no válida.";
            return RedirectToAction("Index", "Home");
        }

        var fromAddress = new MailAddress(smtpUsername, "Lucia Soria");
        var toAddress = new MailAddress(smtpUsername, "Lucia Soria");
        string subject = $"Consulta de {name}";

        var smtp = new SmtpClient
        {
            Host = smtpHost,
            Port = smtpPort,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(smtpUsername, smtpPassword)
        };

        try
        {
            using var messageToSend = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = $"Nombre: {name}\nCorreo: {email}\nMensaje:\n{message}",
                IsBodyHtml = false
            };

            await smtp.SendMailAsync(messageToSend);
            TempData["Success"] = "El correo fue enviado correctamente.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Hubo un problema al enviar el correo: {ex.Message}";
        }

        return RedirectToAction("Index", "Home");
    }
}
