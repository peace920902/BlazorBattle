using System.ComponentModel.DataAnnotations;

namespace BlazorBattle.Shared
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Please enter a username")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}