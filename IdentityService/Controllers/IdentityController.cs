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

namespace IdentityService.Controllers
{
    public class IdentityController : APIController
    {
        private IUserService _userService;
        
        public IdentityController(IUserService userService)
        {
            _userService = userService;

        }



        [HttpPost("authenticateLogin")]
        public IActionResult AuthenticateLogin(AuthenticateRequest model)
        {
            var response = _userService.AuthenticateLogin(model);

            if (response.id == 0)
                return BadRequest(new { message = response.type });

            return Ok(response);
        }



        [HttpPost("Registration")]
        public IActionResult  Register(Registration model)
        {
            var response = _userService.Create(model);
            if(response.email_address == null)
            {
                return BadRequest();
            }
            else
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(response.email_address);
                mail.From = new MailAddress("enotififymoko@gmail.com", "User Creation", System.Text.Encoding.UTF8);
                mail.Subject = "This mail is send from asp.net application";
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = "<a href='#' > button </a>";
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("enotififymoko@gmail.com", "Tade@123");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
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

        //public IdentityController(IRegistrationService registrationService, IOptions<AppSettings> appsetting)
        //{
        //    //_userService = userService;
        //    _RegistrationService = registrationService;

        //}

        //[HttpPost("Registration")]
        //public IActionResult Register(Registration model)
        //{
        //    var user = new User
        //    {
        //        Username = model.Username,
        //        email_address = model.email_address,
        //        active = model.active

        //    };
        //    var result = _RegistrationService.Registration(user);

        //    return ((IActionResult)result);
        //}




    }
}
