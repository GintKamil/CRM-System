using CRMSystem.Shared.Entities;

namespace CRMSystem.Shared.DTO.Api
{
    public class CustomerCreateApiDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Company { get; set; }

        public CustomerStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedById { get; set; }

        public CustomerCreateApiDto() { }

        public CustomerCreateApiDto(string name, string email, string phone, string company, int status, DateTime createdAt, int createdById)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Company = company;
            Status = StatusSwitch(status);
            CreatedAt = createdAt;
            CreatedById = createdById;
        }

        private CustomerStatus StatusSwitch(int status)
        {
            switch(status)
            {
                case 1: return CustomerStatus.Negotiation;
                case 2: return CustomerStatus.Client;
                case 3: return CustomerStatus.Archived;
            }
            return CustomerStatus.Negotiation;
        }
    }
}
