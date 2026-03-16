namespace CRMSystem.Shared.Entities
{
    public interface ITaskRepository
    {
        public Task<TaskM> CreateTask(TaskM task);
        public Task<TaskM> UpdateTask(TaskM task);
        public Task<TaskM> GetTask(int id);
        public Task<List<TaskM>> GetAllTask();
        public Task<List<TaskM>> GetAllTaskById(int id);
        public Task<TaskM> DeleteTask(int id);
        public Task<List<TaskM>> GetCreatedByUser(int userId);
        public Task<List<TaskM>> GetAssignedByUser(int userId);
    }
}
