using Assignment4.Core;
using Assignment4.Entities;
using System.Linq;

namespace Assignment4.Entities
{
    public class TaskRepository
    {
        private readonly KanbanContext _context;

        public TaskRepository(KanbanContext context)
        {
            _context = context;
        }
        public (Response Response, int TaskId) Create(TaskCreateDTO task)
        {
            var entity = new Task
            {
                Title = task.Title,
                AssignedTo = UserRepository.Read(task.AssignedToId),
                State = State.New,
                Tags = task.Tags
            };

            _context.Add(entity);

            _context.SaveChanges();

            return (Response.Created, entity.TaskID);
        }

        public IReadOnlyCollection<TaskDTO> ReadAll()
        {

        }

        public IReadOnlyCollection<TaskDTO> ReadAllRemoved()
        {

        }

        public IReadOnlyCollection<TaskDTO> ReadAllByTag(string tag)
        {

        }

        public IReadOnlyCollection<TaskDTO> ReadAllByUser(int userId)
        {

        }

        public IReadOnlyCollection<TaskDTO> ReadAllByState(State state)
        {

        }
        public TaskDetailsDTO Read(int taskId)
        {
            var tasks = from c in _context.Task
                             where c.TaskID == taskId
                             select new TaskDTO(
                                 c.Id,
                                 c.Title,
                                 c.AssignedToName,
                                 c.Tags,
                                 c.State
                                );
            return tasks.FirstOrDefault();
        }
        public Response Update(TaskUpdateDTO task)
        {

        }

        public Response Delete(int taskId)
        {
            var entity = _context.Task.Find(taskId);

            if(entity == null)
            {
                return Response.NotFound;
            }

            switch(entity.State)
            {
                case State.New:
                    _context.Task.Remove(entity);
                    _context.SaveChanges();

                    return Response.Deleted;
                case State.Active:
                    entity.State = State.Removed;
                    return Response.Deleted;
                default:
                    return Response.Conflict;
            }
        }
    }
}
