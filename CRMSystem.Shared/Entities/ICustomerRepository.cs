using CRMSystem.Modules.Auth.Domain.Entities;
using CRMSystem.Shared.DTO.Api;

namespace CRMSystem.Shared.Entities
{
    public interface ICustomerRepository
    {
        public Task<Customer> CreateCustomer(Customer customer);
        public Task<Customer> UpdateCustomer(Customer customerData);
        public Task<Customer> GetCustomer(int id);
        public Task<Customer> GetCustomerEmail(string email);
        public Task<List<Customer>> GetAllCustomer();
        public Task<Customer> DeleteCustomer(int id);
        public Task<List<Customer>> GetAllCustomerById(int id);
        public Task<List<CustomerApiDto>> GetAllCustomerApi();
    }
}
