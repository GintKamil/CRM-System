using CRMSystem.Modules.Auth.Domain.Entities;
using CRMSystem.Shared.Entities;
using System.Diagnostics.CodeAnalysis;

namespace CRMSystem.Shared.DTO.Api
{
    public class CustomerApiDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Company { get; set; }

        public CustomerStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public UserDto CreatedBy { get; private set; }

        public CustomerApiDto() { }

        public CustomerApiDto(int id, string name, string email, string phone, string company, CustomerStatus status, DateTime createdAt, UserDto createdBy)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Company = company;
            Status = status;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
        }
    }
}

