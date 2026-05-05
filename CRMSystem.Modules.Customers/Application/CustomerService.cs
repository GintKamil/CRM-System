using CRMSystem.Modules.Auth.Domain.Entities;
using CRMSystem.Shared.DTO.Api;
using CRMSystem.Shared.Entities;
using System.Threading.Tasks;

namespace CRMSystem.Modules.Customers.Application
{
    public interface ICustomerService
    {
        public Task<Customer> CreateCustomer(Customer customer);
        public Task<Customer> UpdateCustomer(CustomerCreateViewModel customerData);
        public Task<Customer> GetCustomer(int id);
        public Task<List<Customer>> GetAllCustomer();
        public Task<Customer> DeleteCustomer(int id);
        public Customer СustomerСonversion(CustomerCreateViewModel model, User user);
        public CustomerCreateViewModel СustomerСonversion(Customer model);
        public Task<List<Customer>> GetForUser(int id, string role);
        public Task<Customer> CustomerConvApi(CustomerCreateApiDto CustomerApiDto);
        public CustomerApiDto CustomerConvApi(Customer customer);
        public Task<List<CustomerApiDto>> CustomerAllConvApi();
    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        public CustomerService(ICustomerRepository customerRepository, IUserRepository userRepository)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            await _customerRepository.CreateCustomer(customer);
            return customer;
        }
        public async Task<Customer> UpdateCustomer(CustomerCreateViewModel customerData)
        {
            var customer = await _customerRepository.GetCustomerEmail(customerData.Email);
            var user = await _userRepository.GetId(customer.CreatedById);
            var customerEdit = СustomerСonversion(customerData, user);
            customerEdit.Id = customer.Id;
            await _customerRepository.UpdateCustomer(customerEdit);
            return customerEdit;
        }
        public async Task<Customer> GetCustomer(int id)
        {
            return await _customerRepository.GetCustomer(id);
        }
        public async Task<List<Customer>> GetAllCustomer()
        {
            return await _customerRepository.GetAllCustomer();
        }

        public async Task<Customer> DeleteCustomer(int id)
        {
            return await _customerRepository.DeleteCustomer(id);
        }
        public Customer СustomerСonversion(CustomerCreateViewModel model, User user)
        {
            return new Customer(
                model.Name,
                model.Email,
                model.Phone,
                model.Company,
                model.Status,
                DateTime.UtcNow,
                user
                );
        }
        public CustomerCreateViewModel СustomerСonversion(Customer model)
        {
            return new CustomerCreateViewModel(
                model.Name,
                model.Email,
                model.Phone,
                model.Company,
                model.Status
                );
        }

        public async Task<List<Customer>> GetForUser(int id, string role)
        {
            if (role == UserRole.Admin.ToString())
                return await _customerRepository.GetAllCustomer();

            return await _customerRepository.GetAllCustomerById(id);
        }

        public async Task<Customer> CustomerConvApi(CustomerCreateApiDto customerApi)
        {
            var user = await _userRepository.GetId(customerApi.CreatedById);
            
            if (user == null) 
                throw new Exception("Пользователя не существует");
            
            return new Customer
                (
                    customerApi.Name,
                    customerApi.Email,
                    customerApi.Phone,
                    customerApi.Company,
                    customerApi.Status,
                    DateTime.UtcNow,
                    user
                );
        }

        public CustomerApiDto CustomerConvApi(Customer customer)
        {
            if (customer == null) return null;

            return new CustomerApiDto
                (
                    customer.Id,
                    customer.Name,
                    customer.Email,
                    customer.Phone,
                    customer.Company,
                    customer.Status,
                    customer.CreatedAt,
                    new UserDto(
                        customer.CreatedBy.Id,
                        customer.CreatedBy.Name,
                        customer.CreatedBy.Role
                    )
                );
        }
        public async Task<List<CustomerApiDto>> CustomerAllConvApi()
        {
            return await _customerRepository.GetAllCustomerApi();
        }
    }
}
