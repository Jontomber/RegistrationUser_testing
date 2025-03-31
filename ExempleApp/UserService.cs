using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserManger.Repository;

namespace UserManger.Service;
public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public List<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers(); // Use the method
    }

    public RegistrationResponse RegisterUser(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (!IsValidUsername(user.Name))
            throw new ArgumentException("Invalid name format. Username must be 5-20 alphanumeric characters.");

        if (!IsValidEmail(user.Email))
            throw new ArgumentException("Invalid email format.");

        if (!IsValidPassword(user.Password))
            throw new ArgumentException("Password must be at least 8 characters long and contain at least one special character.");

        if (_userRepository.IsNameTaken(user.Name))
            throw new ArgumentException("Username is already taken.");


        // All validations passed, add the user
        _userRepository.AddUser(user);

        return new RegistrationResponse
        {
            Name = user.Name,

            Message = "User registered successfully."
          
        };
    }

    public bool IsValidUsername(string name)
    {
        // Check if username is null or empty
        if (string.IsNullOrEmpty(name))
            return false;

        // Check if username follows the pattern (5-20 alphanumeric characters)
        return Regex.IsMatch(name, @"^[a-zA-Z0-9]{5,20}$");
    }

    public bool IsValidEmail(string email)
    { 
        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[a-zA-Z]{2,}$"); // Regular expression for email validation
        return regex.IsMatch(email); // Return true if it matches
    }


    public bool IsValidPassword(string password)
    {
        return Regex.IsMatch(password, @"^(?=.*[!""#$%&'()*+,-./:;<=>?@\[\]^_{|}~]).{8,}$");
    }

    public bool IsNameTaken(string name)
    {
        return _userRepository.IsNameTaken(name);
    }
    
    public class RegistrationResponse
    {
        public string Name { get; set; }
        public string Message { get; set; }
    }

}



