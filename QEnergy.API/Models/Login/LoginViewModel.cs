using System.ComponentModel.DataAnnotations;

namespace QEnergy.API.Models.Login
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
