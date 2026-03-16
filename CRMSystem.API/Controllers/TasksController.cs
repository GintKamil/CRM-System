using CRMSystem.Modules.Auth.Application.Services;
using CRMSystem.Modules.Customers.Application;
using CRMSystem.Modules.Tasks.Application;
using CRMSystem.Modules.Tasks.DTO;
using CRMSystem.Modules.Tasks.Model;
using CRMSystem.Shared.DTO.Api;
using CRMSystem.Shared.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CRMSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/tasks/")]
    public class TasksController : ControllerBase
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

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var user = UserContext.FromClaims(User);
            var data = await _taskService.GetTasksForIndex(user);

            var dto = new TaskIndexApiDto
            {
                AllTasks = data.AllTasks?.Select(_taskService.MapToDto).ToList(),
                AssignedToMe = data.AssignedToMe?.Select(_taskService.MapToDto).ToList(),
                CreatedByMe = data.CreatedByMe?.Select(_taskService.MapToDto).ToList(),
            };
            return Ok(dto);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(TaskCreateApiDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var task = _taskService.TaskConversion(dto, userId);

            await _taskService.Create(task);
            return Ok("Добавлена задача - " + dto.Title);
        }

        [HttpGet("createget")]
        public async Task<IActionResult> CreateGet()
        {
            var get = new CreateGetApi();
            get.Customers = await GetCustomers();
            get.Users = await GetUsers();
            return Ok(get);
        }

        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskService.Delete(id);
            return Ok("Удалена задача - " + id);
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
