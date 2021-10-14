using Assignment4.Core;
using Assignment4.Entities;
using System.Linq;
using System.Collections.Generic;

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
                Description = task.Description,
                State = State.New,
                Tags = task.Tags
            };

            _context.Add(entity);

            _context.SaveChanges();

            return (Response.Created, entity.TaskID);
        }

        public IReadOnlyCollection<TaskDTO> ReadAll()
        {
            var tasks = from c in _context.Task
                             select new TaskDTO(
                                 c.Id,
                                 c.Title,
                                 c.AssignedToName,
                                 c.Tags,
                                 c.State
                                );
            return tasks;
        }

        public IReadOnlyCollection<TaskDTO> ReadAllRemoved()
        {
            var tasks = from c in _context.Task
                            where c.State == State.Removed
                            select new TaskDTO(
                                 c.Id,
                                 c.Title,
                                 c.AssignedToName,
                                 c.Tags,
                                 c.State
                                );
            return tasks;
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByTag(string tag)
        {
            var tasks = from c in _context.Task
                             where c.Tags.Contains(tag)
                             select new TaskDTO(
                                 c.Id,
                                 c.Title,
                                 c.AssignedToName,
                                 c.Tags,
                                 c.State
                                );
            return tasks;
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByUser(int userId)
        {
            var tasks = from c in _context.Task
                             where c.AssignedTo == _context.User.Find(userId)
                             select new TaskDTO(
                                 c.Id,
                                 c.Title,
                                 c.AssignedToName,
                                 c.Tags,
                                 c.State
                                );
            return tasks;
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByState(State state)
        {
            var tasks = from c in _context.Task
                             where c.State == state
                             select new TaskDTO(
                                 c.Id,
                                 c.Title,
                                 c.AssignedToName,
                                 c.Tags,
                                 c.State
                                );
            return tasks;
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
