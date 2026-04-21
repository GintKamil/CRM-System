using CRMSystem.Modules.Auth.Domain.Entities;
using CRMSystem.Shared.Entities;

namespace CRMSystem.Shared.Comment
{
    public class TaskComment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int TaskId { get; set; }
        public TaskM Task { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
