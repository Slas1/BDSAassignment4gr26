using Assignment4.Core;

namespace Assignment4.Entities
{
    public class UserRepository
    {
        (Response Response, int UserId) Create(UserCreateDTO user);
        IReadOnlyCollection<UserDTO> ReadAll();
        UserDTO Read(int userId);
        Response Update(UserUpdateDTO user);
        Response Delete(int userId, bool force = false);
    }
}