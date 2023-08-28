using AutoMapper.Internal;
using EntityLayer.Concrete;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Threading.Tasks;
using TraversalCoreProje.Models;

namespace TraversalCoreProje.Controllers
{
    [AllowAnonymous]
    public class PasswordChangeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public PasswordChangeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Mail);
            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var passwordResetTokenLink = Url.Action("ResetPassword", "PasswordChange", new
            {
                userId = user.Id,
                token = passwordResetToken
            },HttpContext.Request.Scheme);
            

            MimeMessage mimeMessage = new();

            //Gönderecek olan kişinin bilgileri -başlangıç (Mesajı gönderen)
            MailboxAddress mailboxAddress = new MailboxAddress("Admin", "traversal.project@gmail.com");
            mimeMessage.From.Add(mailboxAddress);
            //Gönderecek olan kişinin bilgileri -bitiş

            //Alacak olan kişinin bilgileri -başlangıç (Mesajı alan)
            MailboxAddress mailboxAddressTo = new("User", forgetPasswordViewModel.Mail);
            mimeMessage.To.Add(mailboxAddressTo);
            //Alacak olan kişinin bilgileri -bitiş

            //Mesaj içeriği-başlangıç
            var bodybuilder = new BodyBuilder();
            bodybuilder.TextBody = passwordResetTokenLink;
            mimeMessage.Body = bodybuilder.ToMessageBody();
            //mimeMessage.Body = bodybuilder.ToMessageBody();
            //Mesaj içeriği-bitiş

            //Konu
            mimeMessage.Subject = "Şifre Değişiklik Talebi";

            //SmptClient => Simple Mail Transfer Protokol Sunucusu
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("traversal.project@gmail.com", "qlowczyubagyyjee"); // Alıcı maili,gmailde oluşturduğumuz uygulama şifresi
            client.Send(mimeMessage);
            client.Disconnect(true);
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string userid,string token)
        {
            TempData["userid"] = userid;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var userid = TempData["userid"];
            var token = TempData["token"];
            if(userid == null || token == null)
            {
                //error message
            }
            var user = await _userManager.FindByIdAsync(userid.ToString());
            var result = await _userManager.ResetPasswordAsync(user, token.ToString(),resetPasswordViewModel.Password); // Şifreyi güncellemek için gerekli olan metod(User,Token,yeni şifre)
            if(result.Succeeded)
            {
                return RedirectToAction("SignIn", "Login");
            }
            return View();
        }

    }
}
