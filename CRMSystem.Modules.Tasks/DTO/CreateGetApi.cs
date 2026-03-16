using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRMSystem.Modules.Tasks.DTO
{
    public class CreateGetApi
    {
        public List<SelectListItem> Customers { get; set; } = new();
        public List<SelectListItem> Users { get; set; } = new();
    }
}
