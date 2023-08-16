using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using TraversalCoreProje.Models;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(MailRequest mailRequest)
        {
            MimeMessage mimeMessage = new();

            //Gönderecek olan kişinin bilgileri -başlangıç (Mesajı gönderen)
            MailboxAddress mailboxAddress = new MailboxAddress("Admin", "traversal.project@gmail.com");
            mimeMessage.From.Add(mailboxAddress);
            //Gönderecek olan kişinin bilgileri -bitiş

            //Alacak olan kişinin bilgileri -başlangıç (Mesajı alan)
            MailboxAddress mailboxAddressTo = new("User", mailRequest.RecieverMail);
            mimeMessage.To.Add(mailboxAddressTo);
            //Alacak olan kişinin bilgileri -bitiş

            //Mesaj içeriği-başlangıç
            var bodybuilder = new BodyBuilder();
            bodybuilder.TextBody = mailRequest.Body;
            mimeMessage.Body = bodybuilder.ToMessageBody();
            //Mesaj içeriği-bitiş
            
            //Konu
            mimeMessage.Subject = mailRequest.Subject;

            //SmptClient => Simple Mail Transfer Protokol Sunucusu
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("traversal.project@gmail.com", "qlowczyubagyyjee"); // Alıcı maili,gmailde oluşturduğumuz uygulama şifresi
            client.Send(mimeMessage);
            client.Disconnect(true);
            return View();
        }
    }
}
