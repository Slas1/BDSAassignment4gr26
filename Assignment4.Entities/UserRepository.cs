using System.Collections.Generic;
using Assignment4.Core;
using System.Linq;

namespace Assignment4.Entities

{
    public class UserRepository : IUserRepository
    {
        private readonly KanbanContext _context;

        public UserRepository(KanbanContext context)
        {
            _context = context;
        }
        public (Response Response, int UserId) Create(UserCreateDTO user)
        {
            var entity = new User
            {
                Name = user.Name,
                Email = user.Email
            };

            _context.Add(entity);

            _context.SaveChanges();

            return (Response.Created, entity.UserID);
        }

        public Response Delete(int userId, bool force = false)
        {
            var entity = _context.User.Find(userId);

            if(entity == null)
            {
                return Response.NotFound;
            }

            /*if (entity.Tasks.Count() != 0 && force == false)
            {
                return Response.Conflict;
            }*/

            _context.User.Remove(entity);
            _context.SaveChanges();

            return Response.Deleted;
        }

        public UserDTO Read(int userId)
        {
            var users = from c in _context.User
                             where c.UserID == userId
                             select new UserDTO(
                                 c.UserID,
                                 c.Name,
                                 c.Email
                                );
            return users.FirstOrDefault();
        }

        public IReadOnlyCollection<UserDTO> ReadAll()
        {
            return _context.User
                    .Select(c => new UserDTO(c.UserID, c.Name, c.Email))
                    .ToList().AsReadOnly();
        }

        public Response Update(UserUpdateDTO user)
        {
            var entity = _context.User.Find(user.Id);

            if (entity == null)
            {
                return Response.NotFound;
            }

            entity.UserID = user.Id;
            entity.Name = user.Name;
            entity.Email = user.Email;

            _context.SaveChanges();

            return Response.Updated;
        }
    }
}