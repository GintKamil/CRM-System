using CRMSystem.Infrastructure.DB;
using CRMSystem.Modules.Auth.Domain.Entities;
using CRMSystem.Shared.DTO.Api;
using CRMSystem.Shared.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CRMSystem.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataBaseContext _context;

        public CustomerRepository(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<Customer> CreateCustomer(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка - " + ex.ToString());
            }
            return customer;
        }
        public async Task<Customer> UpdateCustomer(Customer customerData)
        {
            Customer? customer = await _context.Customers.FirstOrDefaultAsync(u => u.Id == customerData.Id);

            if (customer == null)
                return null;
            
            customer.Name = customerData.Name;
            customer.Email = customerData.Email;
            customer.Phone = customerData.Phone;
            customer.Company = customerData.Company;
            customer.Status = customerData.Status;

            await _context.SaveChangesAsync();
            return customer;
        }
        public async Task<Customer> GetCustomer(int id)
        {
            return await _context.Customers.Include(c => c.CreatedBy).FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<List<Customer>> GetAllCustomer()
        {
            return await _context.Customers.Include(c => c.CreatedBy).ToListAsync();
        }
        public async Task<Customer> DeleteCustomer(int id)
        {
            Customer? customer = await _context.Customers.FirstOrDefaultAsync(u => u.Id == id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<List<Customer>> GetAllCustomerById(int id)
        {
            return await _context.Customers
                .Where(c => c.CreatedById == id)
                .Select(c => new Customer(c.Name, c.Email, c.Phone, c.Company, c.Status, c.CreatedAt, c.CreatedBy))
                .ToListAsync();
        }
        public async Task<List<CustomerApiDto>> GetAllCustomerApi()
        {
            return await _context.Customers
                .Select(c => new CustomerApiDto(
                    c.Id,
                    c.Name,
                    c.Email,
                    c.Phone,
                    c.Company,
                    c.Status,
                    c.CreatedAt,
                    new UserDto(
                        c.CreatedBy.Id,
                        c.CreatedBy.Name,
                        c.CreatedBy.Role
                    )
            )).ToListAsync();
        }
    }
}
