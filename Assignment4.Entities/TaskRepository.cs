using Assignment4.Core;

namespace Assignment4.Entities
{
    public class TaskRepository : ITaskRepository
    {
        (Response Response, int TaskId) Create(TaskCreateDTO task);
        IReadOnlyCollection<TaskDTO> ReadAll();
        IReadOnlyCollection<TaskDTO> ReadAllRemoved();
        IReadOnlyCollection<TaskDTO> ReadAllByTag(string tag);
        IReadOnlyCollection<TaskDTO> ReadAllByUser(int userId);
        IReadOnlyCollection<TaskDTO> ReadAllByState(State state);
        TaskDetailsDTO Read(int taskId);
        Response Update(TaskUpdateDTO task);
        Response Delete(int taskId);

    }
}
