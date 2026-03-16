using CRMSystem.Modules.Auth.Application.Services;
using CRMSystem.Modules.Customers.Application;
using CRMSystem.Shared.DTO.Api;
using CRMSystem.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRMSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;

        public CustomersController(ICustomerService customerService, IUserService userService)
        {
            _customerService = customerService;
            _userService = userService;
        }

        [HttpGet("get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var customerDto = _customerService.CustomerConvApi(await _customerService.GetCustomer(id));
            return Ok(customerDto);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.CustomerAllConvApi();
            
            return Ok(customers);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CustomerCreateApiDto customerApi)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            customerApi.CreatedById = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var customer = _customerService.CustomerConvApi(customerApi);
            
            return Ok("Создан новый клиент - " + customer.Id);
        }

        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.DeleteCustomer(id);
            return Ok("Удален клиент - " + customer.Id);
        }

        [HttpPost("update/{id:int}")]
        public async Task<IActionResult> Update(int id, CustomerCreateApiDto customerUpdate)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var customerUp = await _customerService.CustomerConvApi(customerUpdate);
            customerUp.Id = id;
            var customer = await _customerService.UpdateCustomer(customerUp);

            if (customer == null) return BadRequest("Клиента с таким id не существует");

            return Ok("Обновлен клиент - " + customerUp.Id);
        }
    }
}
