using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserDataFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FcmNotificationController : ControllerBase
    {
        // POST api/SendNotification
        /// <summary>
        /// This Method used to send push notification to android subscribed devices
        /// </summary>
        /// <param name="request"></param>
        /// <returns>string</returns>
        [HttpPost("SendNotification")]
        public async Task<IActionResult> Post([FromBody] FcmMessageRequest request)
        {
            try
            {
                var notification = new Notification()
                {
                    Title = request.Title,
                    Body = request.Body,
                };

                if (request.ImageUri!=null&&!string.IsNullOrEmpty(request.ImageUri) && Uri.IsWellFormedUriString(request.ImageUri, UriKind.Absolute))
                    notification.ImageUrl = request.ImageUri;

                var message = new Message()
                {
                    Notification = notification,
                    Data = new Dictionary<string, string>()
                    {
                        ["From"]="Logesh",
                        ["To"]="Dad"
                    },
                    Token = "fJ625Hr9SQeq7va_7xHq2e:APA91bHHgIaVSSFW87p69-uZrX3ku5yWKBSQHOcl5VOLhUIn_TOOt9rlrDIk_vPalYQC2EKBCw7xU093O0YtyL2uJNviZZQZCiaGp7rTkOvNs0AhAmNpDuDksWNXeYX0WEAjbMWOKIkN",
                };
                var result = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                return Ok(result); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
    }
}
