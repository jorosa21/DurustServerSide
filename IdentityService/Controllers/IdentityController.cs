using IdentityService.Helper;
using IdentityService.Model;
using IdentityService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IdentityService.Entities;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

using Microsoft.Extensions.Logging;

namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private IUserService _userService;

        private EmailSender email;
        private Default_Url url;

        public IdentityController(IUserService userService, IOptions<EmailSender> appSettings, IOptions<Default_Url> settings)
        {

            _userService = userService;

            email = appSettings.Value;
            url = settings.Value;
        }


        [HttpPost("Registration")]
        public IActionResult Register(Registration model)
        {
            var response = _userService.Create(model);
            if (response.email_address == null)
            {
                return Ok();
            }
            else
            {


                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(response.email_address);
                mail.From = new MailAddress(email.email_username, email.email_name, System.Text.Encoding.UTF8);
                mail.Subject = "This mail is send from asp.net application";
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = "<a href='" + url.name + "/login/" + response.id + "' > button </a>";
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential(email.email_username, email.email_password);
                client.Port = email.port;
                client.Host = email.host;

                client.EnableSsl = true;
                try
                {
                    client.Send(mail);
                    //Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
                }
                catch (Exception ex)
                {
                    Exception ex2 = ex;
                    string errorMessage = string.Empty;
                    while (ex2 != null)
                    {
                        errorMessage += ex2.ToString();
                        ex2 = ex2.InnerException;
                    }
                    //Page.RegisterStartupScript("UserMsg", "<script>alert('Sending Failed...');if(alert){ window.location='SendMail.aspx';}</script>");
                }

            }
            return Ok();
        }



        [HttpPost("Verification")]
        public IActionResult Verification(VerificationRequest model)
        {
            var response = _userService.Verification(model);
            if (response.guid == null)
            {
                return BadRequest();
            }
            return Ok();
        }




    }

}
