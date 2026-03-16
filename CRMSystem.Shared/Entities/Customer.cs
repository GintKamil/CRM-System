using CRMSystem.Modules.Auth.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CRMSystem.Shared.Entities
{
    public class Customer : BaseModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Company { get; set; }

        public CustomerStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedById { get; private set; }
        
        public required User CreatedBy { get; set; }
        
        public Customer() { }

        [SetsRequiredMembers]
        public Customer(string name, string email, string phone, string company, CustomerStatus status, DateTime createdAt, User createdBy)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Company = company;
            Status = status;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            CreatedById = createdBy.Id;
        }
    }

    public enum CustomerStatus
    {
        Negotiation = 1,
        Client = 2,
        Archived = 3
    }
}
