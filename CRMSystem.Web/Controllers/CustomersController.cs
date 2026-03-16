using CRMSystem.Modules.Auth.Application.Services;
using CRMSystem.Modules.Customers.Application;
using CRMSystem.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRMSystem.Web.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;

        public CustomersController(ICustomerService customerService, IUserService userService)
        {
            _customerService = customerService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            string role = User.FindFirst(ClaimTypes.Role)?.Value;

            var customers = await _customerService.GetForUser(userId, role);
            //var customers = await _customerService.GetAllCustomer();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string? userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userService.GetEmail(userEmail);

            var customer = _customerService.СustomerСonversion(model, user);

            await _customerService.CreateCustomer(customer);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerService.GetCustomer(id);
            var customerView = _customerService.СustomerСonversion(customer);
            return View(customerView);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);

            await _customerService.UpdateCustomer(customer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var customer = await _customerService.GetCustomer(id);
            return View(customer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.DeleteCustomer(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
