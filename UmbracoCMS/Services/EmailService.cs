using Azure.Messaging.ServiceBus;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Text;
using UmbracoCMS.Models;

namespace UmbracoCMS.Services;

public class EmailService
{
    private readonly ServiceBusClient _client;
    private readonly ServiceBusSender _sender;

    public EmailService(string connectionString, string queue)
    {
        _client = new ServiceBusClient(connectionString);
        _sender = _client.CreateSender(queue);
    }

    public async Task PublishAsync(string message, string messageType = null!)
    {
        try
        {
            var payload = new ServiceBusMessage(message);

            if (payload != null)
                payload.ApplicationProperties.Add("messageType", messageType);
            
            await _sender.SendMessageAsync(payload);
        }
        catch (Exception ex) 
        {
        }
    }

    public async Task<bool> SendEmailConfirmationAsync(EmailDto dto)
    {
        try
        {
            var message = JsonConvert.SerializeObject(dto);

            await PublishAsync(message);

            return true;
        }
        catch
        {

        }

        return false;
    }

    public async Task <bool> CallbackFormEmailConfirmationAsync(CallbackFormModel model)
    {
        #region EMAIL HTML
        var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"en\">");
            sb.AppendLine("<head>");
            sb.AppendLine("    <meta charset=\"UTF-8\">");
            sb.AppendLine("    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">");
            sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            sb.AppendLine("    <style>");
            sb.AppendLine("        body {");
            sb.AppendLine("            font-family: Arial, sans-serif;");
            sb.AppendLine("            background-color: #f4f4f4;");
            sb.AppendLine("            margin: 0;");
            sb.AppendLine("            padding: 0;");
            sb.AppendLine("        }");
            sb.AppendLine("        .container {");
            sb.AppendLine("            width: 100%;");
            sb.AppendLine("            max-width: 600px;");
            sb.AppendLine("            margin: 0 auto;");
            sb.AppendLine("            background-color: #ffffff;");
            sb.AppendLine("            padding: 20px;");
            sb.AppendLine("            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);");
            sb.AppendLine("        }");
            sb.AppendLine("        .header {");
            sb.AppendLine("            background-color: #394a41;");
            sb.AppendLine("            padding: 20px;");
            sb.AppendLine("            color: #ffffff;");
            sb.AppendLine("            text-align: center;");
            sb.AppendLine("            font-size: 24px;");
            sb.AppendLine("            font-weight: bold;");
            sb.AppendLine("        }");
            sb.AppendLine("        .content {");
            sb.AppendLine("            padding: 20px;");
            sb.AppendLine("            text-align: center;");
            sb.AppendLine("            color: #333333;");
            sb.AppendLine("        }");
            sb.AppendLine("        .content h2 {");
            sb.AppendLine("            color: #394a41;");
            sb.AppendLine("            margin-bottom: 10px;");
            sb.AppendLine("        }");
            sb.AppendLine("        .button {");
            sb.AppendLine("            background-color: #e3d5b7;");
            sb.AppendLine("            color: #394;");
            sb.AppendLine("    </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("    <div class=\"container\">");
            sb.AppendLine("        <!-- Header -->");
            sb.AppendLine("        <div class=\"header\">");
            sb.AppendLine("            Onatrix");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <!-- Content -->");
            sb.AppendLine("        <div class=\"content\">");
            sb.AppendLine("            <h2>Request Received</h2>");
            sb.AppendLine($"            <p>Thank you {model.Name} for getting in touch with us! We have received your request for a call back.</p>");
            sb.AppendLine($"            <p>Our team will get in touch with you shortly to discuss your inquiry about <strong>{model.Inquiry}</strong>.</p>");
            sb.AppendLine("            <p>If you have any additional questions, feel free to reply to this email.</p>");
            sb.AppendLine("            <a href=\"#\" class=\"button\">Visit Our Website</a>");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <!-- Footer -->");
            sb.AppendLine("        <div class=\"footer\">");
            sb.AppendLine("            &copy; 2024 Onatrix. All rights reserved.");
            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
        #endregion

        var htmlBody = sb.ToString();
        var plainText = "";
        var subject = "You will hear from one of our consultants soon!";

        var dto = new EmailDto
        {
            to = model.Email,
            subject = subject,
            htmlBody = htmlBody,
            plainText = plainText,
        };

        var result = await SendEmailConfirmationAsync(dto);

        if (result)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public async Task<bool> OnlineSupportEmailConfirmation(OnlineSupportFormModel model)
    {
        #region EMAIL HTML
        var sb = new StringBuilder();
        sb.AppendLine("<!DOCTYPE html>");
        sb.AppendLine("<html lang=\"en\">");
        sb.AppendLine("<head>");
        sb.AppendLine("    <meta charset=\"UTF-8\">");
        sb.AppendLine("    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">");
        sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
        sb.AppendLine("    <style>");
        sb.AppendLine("        body {");
        sb.AppendLine("            font-family: Arial, sans-serif;");
        sb.AppendLine("            background-color: #f4f4f4;");
        sb.AppendLine("            margin: 0;");
        sb.AppendLine("            padding: 0;");
        sb.AppendLine("        }");
        sb.AppendLine("        .container {");
        sb.AppendLine("            width: 100%;");
        sb.AppendLine("            max-width: 600px;");
        sb.AppendLine("            margin: 0 auto;");
        sb.AppendLine("            background-color: #ffffff;");
        sb.AppendLine("            padding: 20px;");
        sb.AppendLine("            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);");
        sb.AppendLine("        }");
        sb.AppendLine("        .header {");
        sb.AppendLine("            background-color: #394a41;");
        sb.AppendLine("            padding: 20px;");
        sb.AppendLine("            color: #ffffff;");
        sb.AppendLine("            text-align: center;");
        sb.AppendLine("            font-size: 24px;");
        sb.AppendLine("            font-weight: bold;");
        sb.AppendLine("        }");
        sb.AppendLine("        .content {");
        sb.AppendLine("            padding: 20px;");
        sb.AppendLine("            text-align: center;");
        sb.AppendLine("            color: #333333;");
        sb.AppendLine("        }");
        sb.AppendLine("        .content h2 {");
        sb.AppendLine("            color: #394a41;");
        sb.AppendLine("            margin-bottom: 10px;");
        sb.AppendLine("        }");
        sb.AppendLine("        .button {");
        sb.AppendLine("            background-color: #e3d5b7;");
        sb.AppendLine("            color: #394;");
        sb.AppendLine("    </style>");
        sb.AppendLine("</head>");
        sb.AppendLine("<body>");
        sb.AppendLine("    <div class=\"container\">");
        sb.AppendLine("        <!-- Header -->");
        sb.AppendLine("        <div class=\"header\">");
        sb.AppendLine("            Onatrix");
        sb.AppendLine("        </div>");
        sb.AppendLine("        <!-- Content -->");
        sb.AppendLine("        <div class=\"content\">");
        sb.AppendLine("            <h2>Request Received</h2>");
        sb.AppendLine("            <p>Thank you for getting in touch with us! We have received your request for a call back.</p>");
        sb.AppendLine($"            <p>Our team will get in touch with you shortly.</p>");
        sb.AppendLine("            <p>If you have any additional questions, feel free to reply to this email.</p>");
        sb.AppendLine("            <a href=\"#\" class=\"button\">Visit Our Website</a>");
        sb.AppendLine("        </div>");
        sb.AppendLine("        <!-- Footer -->");
        sb.AppendLine("        <div class=\"footer\">");
        sb.AppendLine("            &copy; 2024 Onatrix. All rights reserved.");
        sb.AppendLine("        </div>");
        sb.AppendLine("    </div>");
        sb.AppendLine("</body>");
        sb.AppendLine("</html>");
        #endregion

        var htmlBody = sb.ToString();
        var plainText = "";
        var subject = "You will hear from one of our consultants soon!";

        var dto = new EmailDto
        {
            to = model.Email,
            subject = subject,
            htmlBody = htmlBody,
            plainText = plainText,
        };

        var result = await  SendEmailConfirmationAsync(dto);

        if (result)
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    public async Task<bool> EmailFormEmailConfirmationAsync(EmailFormModel model)
    {
        #region EMAIL HTML
        var sb = new StringBuilder();
        sb.AppendLine("<!DOCTYPE html>");
        sb.AppendLine("<html lang=\"en\">");
        sb.AppendLine("<head>");
        sb.AppendLine("    <meta charset=\"UTF-8\">");
        sb.AppendLine("    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">");
        sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
        sb.AppendLine("    <style>");
        sb.AppendLine("        body {");
        sb.AppendLine("            font-family: Arial, sans-serif;");
        sb.AppendLine("            background-color: #f4f4f4;");
        sb.AppendLine("            margin: 0;");
        sb.AppendLine("            padding: 0;");
        sb.AppendLine("        }");
        sb.AppendLine("        .container {");
        sb.AppendLine("            width: 100%;");
        sb.AppendLine("            max-width: 600px;");
        sb.AppendLine("            margin: 0 auto;");
        sb.AppendLine("            background-color: #ffffff;");
        sb.AppendLine("            padding: 20px;");
        sb.AppendLine("            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);");
        sb.AppendLine("        }");
        sb.AppendLine("        .header {");
        sb.AppendLine("            background-color: #394a41;");
        sb.AppendLine("            padding: 20px;");
        sb.AppendLine("            color: #ffffff;");
        sb.AppendLine("            text-align: center;");
        sb.AppendLine("            font-size: 24px;");
        sb.AppendLine("            font-weight: bold;");
        sb.AppendLine("        }");
        sb.AppendLine("        .content {");
        sb.AppendLine("            padding: 20px;");
        sb.AppendLine("            text-align: center;");
        sb.AppendLine("            color: #333333;");
        sb.AppendLine("        }");
        sb.AppendLine("        .content h2 {");
        sb.AppendLine("            color: #394a41;");
        sb.AppendLine("            margin-bottom: 10px;");
        sb.AppendLine("        }");
        sb.AppendLine("        .button {");
        sb.AppendLine("            background-color: #e3d5b7;");
        sb.AppendLine("            color: #394;");
        sb.AppendLine("    </style>");
        sb.AppendLine("</head>");
        sb.AppendLine("<body>");
        sb.AppendLine("    <div class=\"container\">");
        sb.AppendLine("        <!-- Header -->");
        sb.AppendLine("        <div class=\"header\">");
        sb.AppendLine("            Onatrix");
        sb.AppendLine("        </div>");
        sb.AppendLine("        <!-- Content -->");
        sb.AppendLine("        <div class=\"content\">");
        sb.AppendLine("            <h2>Request Received</h2>");
        sb.AppendLine($"            <p>Thank you {model.Name} for getting in touch with us! We have received your message and will come back to you as soon as possible.</p>");
        sb.AppendLine($"            <p>Your message:</p>");
        sb.AppendLine($"            <p style=\"font-style=italic;\">\"{model.Message}\"</p>");
        sb.AppendLine("            <p>If you have any additional questions, feel free to reply to this email.</p>");
        sb.AppendLine("            <a href=\"#\" class=\"button\">Visit Our Website</a>");
        sb.AppendLine("        </div>");
        sb.AppendLine("        <!-- Footer -->");
        sb.AppendLine("        <div class=\"footer\">");
        sb.AppendLine("            &copy; 2024 Onatrix. All rights reserved.");
        sb.AppendLine("        </div>");
        sb.AppendLine("    </div>");
        sb.AppendLine("</body>");
        sb.AppendLine("</html>");
        #endregion

        var htmlBody = sb.ToString();
        var plainText = "";
        var subject = "We have received your message and will respond to you soon!";

        var dto = new EmailDto
        {
            to = model.Email,
            subject = subject,
            htmlBody = htmlBody,
            plainText = plainText,
        };

        var result = await SendEmailConfirmationAsync(dto);

        if (result)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}