using CRMSystem.Modules.Auth.Application.Services;
using CRMSystem.Modules.Auth.Domain.Entities;
using CRMSystem.Modules.Customers.Application;
using CRMSystem.Modules.Tasks.Application;
using CRMSystem.Modules.Tasks.DTO;
using CRMSystem.Shared.Entities;
using CRMSystem.Shared.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CRMSystem.Web.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;

        public TasksController(ITaskService taskService, ICustomerService customerService, IUserService userService)
        {
            _taskService = taskService;
            _customerService = customerService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var user = UserContext.FromClaims(User);
            var tasksVM = await _taskService.GetTasksForIndex(user);
            return View(tasksVM);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new TaskCreateViewModel
            {
                Customers = await GetCustomers(),
                Users = await GetUsers()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskCreateViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.Customers = await GetCustomers();
                model.Users = await GetUsers();

                return View(model);
            }

            var user = UserContext.FromClaims(User);

            var task = _taskService.TaskConversion(model,
                model.CustomerId,
                model.AssignedToId,
                user.Id);

            await _taskService.Create(task);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var task = await _taskService.GetTask(id);
            return View(task);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public async Task<IActionResult> EditByManager(int id)
        {
            var taskUpdate = _taskService.TaskConvEditManager(
                await _taskService.GetTask(id)
                );
            taskUpdate.TaskId = id;
            taskUpdate.Users = await GetUsers();
            return View(taskUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> EditByManager(UpdateTaskByManagerDto taskUpdate)
        {
            if(!ModelState.IsValid)
            {
                taskUpdate.Users = await GetUsers();
                return View(taskUpdate);
            }

            await _taskService.UpdateByManager(taskUpdate);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditByExecutor(int id)
        {
            var task = await _taskService.GetTask(id);
            var taskUpdate = _taskService.TaskConvEditExecutor(task);
            
            taskUpdate.TaskId = id;
            taskUpdate.CurrentUserId = task.AssignedToId;

            return View(taskUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> EditByExecutor(UpdateTaskByExecutorDto taskUpdate)
        {
            if(!ModelState.IsValid)
                return View(taskUpdate);

            await _taskService.UpdateByExecutor(taskUpdate);
            return RedirectToAction(nameof(Index));
        }

        private async Task<List<SelectListItem>> GetCustomers()
        {
            return (await _customerService.GetAllCustomer())
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name,
                    }).ToList();
        }

        private async Task<List<SelectListItem>> GetUsers()
        {
            return (await _userService.GetAll())
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name,
                    }).ToList();
        }
    }
}
