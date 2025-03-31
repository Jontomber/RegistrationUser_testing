using UserManger.Service;


namespace UserManger.Repository;
public interface IUserRepository
{
    bool AddUser(User user);
    bool IsNameTaken(string name);  // Added this method to check duplicate usernames
    List<User> GetAllUsers();

}

public class UserRepository : IUserRepository // Added this class to check duplicate usernames
{
    public List<User> _users = new List<User>();

    public bool AddUser(User user)
    {

        if (user == null) return false;

        _users.Add(user);
        return true;
    }

    public bool IsNameTaken(string name)
    {
        return _users.Any(u => u.Name == name);
    }

    public List<User> GetAllUsers()
    {
        return _users;
    }
}

