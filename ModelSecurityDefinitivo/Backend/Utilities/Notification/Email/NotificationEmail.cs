using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Microsoft.Extensions.Configuration;

namespace Utilities.Notification.Email;

public class NotificationEmail
{
    private readonly IConfiguration _configuration;
    private readonly string _fromEmail;
    private readonly string _fromName = "RappiGestion";

    public NotificationEmail(IConfiguration configuration)
    {
        _configuration = configuration;
        _fromEmail = _configuration["EmailMessage:email"]!;
    }

    // public async Task SendWelcomeEmailAsync(string to)
    // {
    //     var message = new MimeMessage();
    //     message.From.Add(new MailboxAddress(_fromName, _fromEmail));
    //     message.To.Add(MailboxAddress.Parse(to));
    //     message.Subject = $"🎉 ¡Bienvenido a {_fromName}!";

    //     var htmlBody = $@"
    //     <html>
    //     <head>
    //         <style>
    //             body {{
    //                 font-family: Arial, sans-serif;
    //                 background-color: #f4f4f4;
    //                 margin: 0;
    //                 padding: 0;
    //             }}
    //             .container {{
    //                 max-width: 600px;
    //                 margin: 30px auto;
    //                 background-color: #ffffff;
    //                 padding: 30px;
    //                 border-radius: 8px;
    //                 box-shadow: 0 0 10px rgba(0,0,0,0.1);
    //             }}
    //             h1 {{
    //                 color: #4CAF50;
    //             }}
    //             p {{
    //                 color: #333;
    //                 font-size: 16px;
    //             }}
    //             .footer {{
    //                 margin-top: 30px;
    //                 font-size: 12px;
    //                 color: #aaa;
    //                 text-align: center;
    //             }}
    //         </style>
    //     </head>
    //     <body>
    //         <div class='container'>
    //             <h1>¡Hola 👋!</h1>
    //             <p>¡Bienvenido a <strong>{_fromName}</strong>! Estamos encantados de tenerte con nosotros.</p>
    //             <p>A partir de ahora podrás disfrutar de todas nuestras funcionalidades. Si tienes alguna duda, no dudes en escribirnos.</p>
    //             <p>Gracias por unirte 🙌</p>
    //             <div class='footer'>
    //                 © {DateTime.Now.Year} {_fromName}. Todos los derechos reservados.
    //             </div>
    //         </div>
    //     </body>
    //     </html>";

    //     message.Body = new TextPart("html") { Text = htmlBody };

    //     try
    //     {
    //         using var client = new SmtpClient();
    //         Console.WriteLine("📡 Conectando al servidor SMTP...");
    //         await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

    //         Console.WriteLine("🔐 Autenticando...");
    //         await client.AuthenticateAsync(_fromEmail, _configuration["EmailMessage:password"]);

    //         Console.WriteLine("📤 Enviando mensaje...");
    //         await client.SendAsync(message);

    //         Console.WriteLine("✅ Correo enviado exitosamente.");
    //         await client.DisconnectAsync(true);
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine("❌ Error al enviar el correo:");
    //         Console.WriteLine(ex.Message);
    //         // Aquí podrías lanzar una excepción personalizada si estás en capa de negocio
    //         throw new ApplicationException("Error al enviar el correo de bienvenida.", ex);
    //     }
    // }

    public async Task WelcomenUser(string to, string password)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_fromName, _fromEmail));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = $"🎉 ¡Bienvenido a {_fromName}!";

        var htmlBody = $@"
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    margin: 0;
                    padding: 0;
                }}
                .container {{
                    max-width: 600px;
                    margin: 30px auto;
                    background-color: #ffffff;
                    padding: 30px;
                    border-radius: 8px;
                    box-shadow: 0 0 10px rgba(0,0,0,0.1);
                }}
                h1 {{
                    color: #4CAF50;
                }}
                p {{
                    color: #333;
                    font-size: 16px;
                }}
                .password-box {{
                    background-color: #e0f7fa;
                    border: 2px dashed #4CAF50;
                    padding: 15px 20px;
                    margin: 20px 0;
                    font-size: 20px;
                    font-weight: bold;
                    color: #00796b;
                    text-align: center;
                    border-radius: 8px;
                    letter-spacing: 2px;
                    user-select: all;
                }}
                .footer {{
                    margin-top: 30px;
                    font-size: 12px;
                    color: #aaa;
                    text-align: center;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <h1>¡Hola 👋!</h1>
                <p>¡Bienvenido a <strong>{_fromName}</strong>! Estamos encantados de tenerte con nosotros.</p>
                <p>A partir de ahora podrás disfrutar de todas nuestras funcionalidades. Si tienes alguna duda, no dudes en escribirnos.</p>
                <p>Tu contraseña temporal es:</p>
                <div class='password-box'>{password}</div>
                <p><em>Te recomendamos cambiar esta contraseña en tu perfil para mayor seguridad.</em></p>
                <div class='footer'>
                    © {DateTime.Now.Year} {_fromName}. Todos los derechos reservados.
                </div>
            </div>
        </body>
        </html>";

        message.Body = new TextPart("html") { Text = htmlBody };

        try
        {
            using var client = new SmtpClient();
            Console.WriteLine("📡 Conectando al servidor SMTP...");
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

            Console.WriteLine("🔐 Autenticando...");
            await client.AuthenticateAsync(_fromEmail, _configuration["EmailMessage:password"]);

            Console.WriteLine("📤 Enviando mensaje...");
            await client.SendAsync(message);

            Console.WriteLine("✅ Correo enviado exitosamente.");
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Error al enviar el correo:");
            Console.WriteLine(ex.Message);
            throw new ApplicationException("Error al enviar el correo de bienvenida.", ex);
        }
    }

}
