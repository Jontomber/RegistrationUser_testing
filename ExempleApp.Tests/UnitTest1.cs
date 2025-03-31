using System.Text.RegularExpressions;
using UserManger.Service;
using UserManger.Repository;
namespace UserManger.Tests;

[TestClass]
public class UserServiceTests
{
   
    [TestMethod]
    public void ValidateName_ShouldReturnTrue_ForValidName_HappyPath() // Test to see if the username is valid format
    {
        // Arrange
        var service = new UserService(new UserRepository()); // Create a new instance of the UserService
        string validName = "ValidUser1"; // 10 characters

        // Act
        var isValid = service.IsValidUsername(validName); // Needs to put in aplha numeric characters with 5-20 characters

        // Assert
        Assert.IsTrue(isValid, "Valid username was rejected."); // Message will only display when it fails!
    }

    [TestMethod]
    public void ValidateName_ShouldReturnFalse_ForEmptyName_HappyPath() // Test to see if the username is empty
    {
        // Arrange
        var service = new UserService(new UserRepository()); 
        string validName = ""; // no characters

        // Act
        var isValid = service.IsValidUsername(validName); // Needs to put in aplha numeric characters with 5-20 characters

        // Assert
        Assert.IsFalse(isValid, "Valid username was rejected.");
    }

    [TestMethod]
    public void ValidateName_ShouldReturnFalse_ForTooManyCharachters_HappyPath() // Test to see if the username is too many charachters
    {
        // Arrange
        var service = new UserService(new UserRepository());
        string validName = "NotValidUserTooManyCharachters"; // 25 characters

        // Act
        var isValid = service.IsValidUsername(validName);

        // Assert
        Assert.IsFalse(isValid, "Valid username was rejected.");
    }

    [TestMethod]
    public void ValidateEmail_ShouldReturnTrue_ForValidEmail_HappyPath() // Test to see if the email is valid fromat
    {
        // Arrange
        var service = new UserService(new UserRepository()); 
        string validEmail = "Ost@mail.com"; // Correct email format

        // Act
        var isValid = service.IsValidEmail(validEmail);

        // Assert
        Assert.IsTrue(isValid, "Valid email was rejected.");
    }

    [TestMethod]
    public void ValidateEmail_ShouldReturnFalse_ForEmptyMail_HappyPath() // Test to see if the email is empty
    {
        // Arrange
        var service = new UserService(new UserRepository());
        string validEmail = ""; // Empty email

        // Act
        var isValid = service.IsValidEmail(validEmail);

        // Assert
        Assert.IsFalse(isValid, "Valid email was rejected.");
    }

    [TestMethod]
    public void ValidatePassword_ShouldReturnTrue_ForValidPassword_HappyPath() // Test to see if the password is valid
    {
        // Arrange
        var service = new UserService(new UserRepository());
        string validPassword = "Secure@1"; // Correct password format

        // Act
        var isValid = service.IsValidPassword(validPassword);

        // Assert
        Assert.IsTrue(isValid, "Valid email was rejected.");
    }
    [TestMethod]
    public void ValidatePassword_ShouldReturnFalse_ForEmptyPassword_HappyPath() // Test to see if the password is empty
    {
        // Arrange
        var service = new UserService(new UserRepository());
        string validPassword = ""; // Empty password

        // Act
        var isValid = service.IsValidPassword(validPassword);

        // Assert
        Assert.IsFalse(isValid, "Valid email was rejected.");
    }

    [TestMethod]
    public void ValidateUsername_ShouldReturnFalse_ForDuplicateUsername_HappyPath() // Test to see if the username is already taken
    {
        // Arrange
        var service = new UserService(new UserRepository());
        string takenUsername = "ExistingUser"; // Username that is already taken

        // Act
        bool isUnique = service.IsNameTaken(takenUsername); // Check if the username is already taken

        // Assert
        Assert.IsFalse(isUnique, "Duplicate username was accepted."); 
    }

    [TestMethod]

    public void RegisterUser_ShouldReturnConfirmationMessage()
    {
        // Arrange
        var userRepository = new UserRepository();
        var userService = new UserService(userRepository);
        var user = new User
        {
            Name = "UserValid2",
            Email = "Exemple@mail.com",
            Password = "SecurePass123!"
        };

        // Act
        var response = userService.RegisterUser(user);

        // Assert
        Assert.AreEqual("UserValid2", response.Name);
        Assert.AreEqual("User registered successfully.", response.Message);
    }

    [TestMethod]
    public void RegisterUser_ShouldBeStoredInRepository()
    {
        // Arrange
        var userRepository = new UserRepository();
        var userService = new UserService(userRepository);
        var user = new User
        {
            Name = "NewUser123",
            Email = "Mail@example.com",
            Password = "BestPassword3!"
        };

        // Act
        userService.RegisterUser(user);
        var users = userService.GetAllUsers();

        // Assert
        Assert.AreEqual(1, users.Count);
        Assert.AreEqual("NewUser123", users[0].Name);
        Assert.AreEqual("Mail@example.com", users[0].Email);
    }
}
   