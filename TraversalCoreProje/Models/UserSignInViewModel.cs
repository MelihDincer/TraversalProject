using System.ComponentModel.DataAnnotations;

namespace TraversalCoreProje.Models
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage ="Burayı boş geçemezsiniz...")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Burayı boş geçemezsiniz...")]
        public string Password { get; set; }
    }
}
