
using System.ComponentModel.DataAnnotations;

namespace CRMSystem.Shared.Entities
{
    public class CustomerCreateViewModel
    {
        [Required(ErrorMessage = "Не указано имя пользователя!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана электронная почта!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан телефон!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не указана компания!")]
        public string Company { get; set; }

        [Required]
        public CustomerStatus Status { get; set; }

        public CustomerCreateViewModel() { }

        public CustomerCreateViewModel(string name, string email, string phone, string company, CustomerStatus status)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Company = company;
            Status = status;
        }
    }
}
