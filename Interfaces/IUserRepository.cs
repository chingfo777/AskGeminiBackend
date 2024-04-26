using AskGemini.Models;

namespace AskGemini.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(String id);
        bool UserExist( String userId);
        bool CreateUser( User user );
        bool UpdateUser( User user );
        bool DeleteUser( User user );
        bool Save();
    }
}
