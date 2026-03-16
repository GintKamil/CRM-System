using System.ComponentModel.DataAnnotations;

namespace CRMSystem.Modules.Auth.Domain.Entities
{
    public class User : BaseModel
    {
        [Required(ErrorMessage = "Не указана электронная почта!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указано имя пользователя!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан пароль!")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Длина пароля должна быть от 5 до 50 символов!")]
        public string Password { get; set; }

        public UserRole Role { get; set; }
        public User() { }
        public User(string name, string email, string password, UserRole role = UserRole.Employees)
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role;
        }

        public void SetRole(UserRole role)
        {
            Role = role;
        }

        public override string ToString()
        {
            return $"Имя - {Name}\nЭлектронная почта - {Email}\nПароль - {Password}\nРоль - {Role}";
        }
    }

    public enum UserRole {
        Admin = 1,
        Manager = 2,
        Employees = 3
    }
}
