namespace TraversalCoreProje.Models
{
    public class MailRequest
    {
        public string Name { get; set; }
        public string SenderMail { get; set; } //Gönderenin mail adresi
        public string RecieverMail { get; set; } //Alıcının mail adresi
        public string Subject { get; set; } //Konu
        public string Body { get; set; } //Mesaj içeriği (paragraf)
    }
}
