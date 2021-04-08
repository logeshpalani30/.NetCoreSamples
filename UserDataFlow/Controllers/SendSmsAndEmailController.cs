using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using WebApi.Model;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace UserDataFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendSmsAndEmailController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SendSmsAndEmailController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // POST api/<SendSmsAndEmailController>
        [HttpPost("SendSms")]
        public IActionResult SendSms([FromBody] SendSmsRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.To.Trim()) || string.IsNullOrEmpty(request.Body.Trim()))
                    return BadRequest();

                TwilioClient.Init(_configuration["SmsAccountSid"], _configuration["SmsAuthToken"]);

                var messageOptions = new CreateMessageOptions(to: new PhoneNumber(request.To))
                {
                    Body = request.Body,
                    From = new PhoneNumber(_configuration["SmsFromNumber"]),
                    ValidityPeriod = 120,
                    ForceDelivery = true
                };
                // messageOptions.StatusCallback = new Uri("https://localhost:44371/api/SendSmsAndEmail/SmsStatus");

                var message = MessageResource.Create(messageOptions);

                return Ok(message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"{e.Message}");
            }
        }

        // POST api/SendEmail
        [HttpPost("SendEmail")]
        public IActionResult SendEmail([FromBody]SendEmailRequest request)
        {
            SmtpClient smtp=null;
            try
            {
                if (request.ToAddresses.Count==0 || !IsValidEmail(request.FromAddress.Email) && string.IsNullOrEmpty(request.FromAddress.Name) || string.IsNullOrEmpty(request.Subject.Trim()) || string.IsNullOrEmpty(request.Subject.Trim()))
                    return BadRequest();

                var email = new MimeMessage()
                {
                    From =
                    {
                        new MailboxAddress(request.FromAddress.Name,request.FromAddress.Email)
                    },
                    To =
                    {
                        new GroupAddress("Addresses", (from address in request.ToAddresses where IsValidEmail(address.Email) && !string.IsNullOrEmpty(address.Name) select new MailboxAddress(address.Name, address.Email)).Cast<InternetAddress>().ToList())
                    },
                    Subject = request.Subject,
                    Body = new TextPart(request.IsHtmlBody ? TextFormat.Html : TextFormat.Text)
                    {
                        Text = request.Body
                    },
                };

                if (request.BccAddresses != null && request.BccAddresses.Count > 0)
                    email.Bcc.AddRange((from address in request.BccAddresses where IsValidEmail(address.Email) && !string.IsNullOrEmpty(address.Name) select new MailboxAddress(address.Name, address.Email)).Cast<InternetAddress>().ToList());

                if (request.CcAddresses != null && request.CcAddresses.Any())
                    email.Cc.AddRange((from address in request.CcAddresses where IsValidEmail(address.Email) && !string.IsNullOrEmpty(address.Name) select new MailboxAddress(address.Name, address.Email)).Cast<InternetAddress>().ToList());

                smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587);
                smtp.Authenticate(_configuration["SenderEmailId"], _configuration["SenderEmailPassword"]);
                smtp.Send(email);
                return Ok("Mail send successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"{e.Message}");
            }
            finally
            {
                smtp?.Disconnect(true);
                smtp?.Dispose();
            }
        }

        private static bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(email);
            return match.Success;
        }
    }
}