using System.ComponentModel.DataAnnotations;

namespace CRMSystem.Modules.Auth.Application.DTO
{
    public class LoginDto : BaseModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public LoginDto() { }
        public LoginDto(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public override string ToString()
        {
            return $"Электронная почта - {Email}\nПароль - {Password}";
        }
    }
}
