using System;
using Assignment4.Core;
using Assignment4.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assignment4.Entities
{
    public class TaskRepository //: ITaskRepository
    {
        private readonly KanbanContext _context;

        public TaskRepository(KanbanContext context)
        {
            _context = context;
        }
        public (Response Response, int TaskId) Create(TaskCreateDTO task)
        {
            var listOfTags = new List<Tag>{};
            foreach (string item in task.Tags)
            {
                listOfTags.Add(new Tag{Name = item});
            }
            var entity = new Task
            {
                Title = task.Title,
                Description = task.Description,
                State = State.New,
                Tags = listOfTags
            };

            _context.Add(entity);

            _context.SaveChanges();

            return (Response.Created, entity.TaskID);
        }

        public IReadOnlyCollection<TaskDTO> ReadAll()
        {
            var ListTasks = new List<TaskDTO>();
            foreach (var t in _context.Task)
            {
                ListTasks.Add(new TaskDTO(
                                t.TaskID, 
                                t.Title, 
                                t.AssignedTo.Name, 
                                new ReadOnlyCollection<string>((from n in t.Tags select n.Name).ToList()), 
                                (Assignment4.Core.State)t.State)
                                );
            }
            return ListTasks.AsReadOnly();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllRemoved()
        {
            var tasks = (from t in _context.Task
                            where t.State == State.Removed
                            select new TaskDTO(
                                t.TaskID,
                                t.Title, 
                                t.AssignedTo.Name, 
                                new ReadOnlyCollection<string>((from n in t.Tags select n.Name).ToList()), 
                                (Assignment4.Core.State)t.State
                                )).ToList();
            return tasks;
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByTag(string tag)
        {
            var tasks = (from t in _context.Task
                             where t.Tags.Contains(new Tag{Name = tag}) //Ikke sikker på om dette virker da id'et ikke er det samme
                             select new TaskDTO(
                                 t.TaskID, 
                                 t.Title, 
                                 t.AssignedTo.Name, 
                                 new ReadOnlyCollection<string>((from n in t.Tags select n.Name).ToList()), 
                                 (Assignment4.Core.State)t.State
                                )).ToList();
            return tasks;
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByUser(int userId)
        {
            var tasks = (from t in _context.Task
                             where t.AssignedTo == _context.User.Find(userId)
                             select new TaskDTO(
                                 t.TaskID, 
                                 t.Title, 
                                 t.AssignedTo.Name, 
                                 new ReadOnlyCollection<string>((from n in t.Tags select n.Name).ToList()), 
                                 (Assignment4.Core.State)t.State
                                )).ToList();
            return tasks;
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByState(State state)
        {
            var tasks = (from t in _context.Task
                             where t.State == state
                             select new TaskDTO(
                                 t.TaskID, 
                                 t.Title, 
                                 t.AssignedTo.Name, 
                                 new ReadOnlyCollection<string>((from n in t.Tags select n.Name).ToList()), 
                                 (Assignment4.Core.State)t.State
                                )).ToList();
            return tasks;
        }
        public TaskDetailsDTO Read(int taskId)
        {
            var tasks = from t in _context.Task
                             where t.TaskID == taskId
                             select new TaskDetailsDTO(
                                 t.TaskID, 
                                 t.Title,
                                 t.Description,
                                 new DateTime(),
                                 t.AssignedTo.Name, 
                                 new ReadOnlyCollection<string>((from n in t.Tags select n.Name).ToList()), 
                                 (Assignment4.Core.State)t.State,
                                 new DateTime()
                                );
            return tasks.FirstOrDefault();
        }
        public Response Update(TaskUpdateDTO task)
        {
            var entity = _context.Task.Find(task.Id);

            if (entity == null)
            {
                return Response.NotFound;
            }

            entity.TaskID = task.Id;
            entity.Title = task.Title;
            entity.AssignedTo = _context.User.Find(task.AssignedToId);
            entity.Description = task.Description;
            entity.Tags = new List<Tag>();
            foreach (string t in task.Tags)
            {
                entity.Tags.Add(_context.Tag.Find(t));
            }

            _context.SaveChanges();

            return Response.Updated;
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
