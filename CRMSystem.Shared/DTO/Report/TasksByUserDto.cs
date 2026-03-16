using CRMSystem.Shared.DTO.Api;

namespace CRMSystem.Shared.DTO.Report
{
    public class TasksByUserDto
    {
        public UserDto User { get; set; }
        public int Count { get; set; }
        public TasksByUserDto(UserDto user, int count)
        {
            User = user;
            Count = count;
        }
    }
}
