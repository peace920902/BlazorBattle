using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorBattle.Shared
{
    public class UserRegister
    {
        [Required,EmailAddress]
        public string Email { get; set; }
        [StringLength(16,ErrorMessage = "Username Should less than 16 characters")]
        public string UserName { get; set; }
        public string Bio { get; set; }
        [Required,StringLength(32,MinimumLength = 6)]
        public string Password { get; set; }
        [Compare(nameof(Password),ErrorMessage = "The passwords do not match")]
        public string ConfirmPassword { get; set; }
        public string StartUnitId { get; set; }
        [Range(0, 1000, ErrorMessage = "Please choose a number between 0~1000")]
        public int Bananas { get; set; } = 100;

        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        [Range(typeof(bool), "true", "true", ErrorMessage = "Only confirmed user can play")]
        public bool IsConfirmed { get; set; } = true;
    }
}