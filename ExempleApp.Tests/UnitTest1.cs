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
    public void ValidateName_ShouldReturnFalse_IfSpecialCharachter_NegativePath() // Test to see that special characters can't be used
    {
        // Arrange
        var service = new UserService(new UserRepository()); // create a new instance of the UserService
        string validName = "ValidUser1@"; // 11 characters

        // Act
        var isValid = service.IsValidUsername(validName); // Needs to put in aplha numeric characters with 5-20 characters

        // Assert
        Assert.IsFalse(isValid, "Valid username was rejected.");
    }

    [TestMethod]
    public void ValidateName_ShouldReturnFalse_ForEmptyName_NegativePath() // Test to see if the username is empty or too short
    {
        // Arrange
        var service = new UserService(new UserRepository()); 
        string validName = ""; // No characters

        // Act
        var isValid = service.IsValidUsername(validName); // Needs to put in aplha numeric characters with 5-20 characters

        // Assert
        Assert.IsFalse(isValid, "Valid username was rejected.");
    }

    [TestMethod]
    public void ValidateName_ShouldReturnFalse_ForToManyCharachters_NegativePath() // Test to see if the username is too many charachters
    {
        // Arrange
        var service = new UserService(new UserRepository());
        string validName = "NotValidUserTooManyCharachters"; // 25 characters

        // Act
        var isValid = service.IsValidUsername(validName); // Needs to put in aplha numeric characters with 5-20 characters

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
    public void ValidateEmail_ShouldReturnFalse_ForValidEmail_NegativePath() // Test to see if the email is valid format
    {
        // Arrange
        var service = new UserService(new UserRepository());
        string validEmail = "Ost@.com"; // Incorrecct email format

        // Act
        var isValid = service.IsValidEmail(validEmail);

        // Assert
        Assert.IsFalse(isValid, "Valid email was rejected.");
    }

    [TestMethod]
    public void ValidateEmail_ShouldReturnFalse_ForEmptyMail_NegativePath() // Test to see if the email is empty
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
    public void ValidatePassword_ShouldReturnFalse_ForNotHavingSpecialCharachter_NegativePath() // Test to see if the password has a special charachter
    {
        // Arrange
        var service = new UserService(new UserRepository());
        string validPassword = "Secure1"; // Incorrect password format

        // Act
        var isValid = service.IsValidPassword(validPassword);

        // Assert
        Assert.IsFalse(isValid, "Valid email was rejected.");
    }

    [TestMethod]
    public void ValidatePassword_ShouldReturnFalse_ForEmptyPassword_NegativePath() // Test to see if the password is empty
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
    public void ValidateUsername_ShouldReturnFalse_ForDuplicateUsername_NegativePath() // Test to see if the username is already taken
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

    public void RegisterUser_ShouldReturnConfirmationMessage() // Test to see if the user is registered successfully
    {
        // Arrange
        var userRepository = new UserRepository(); // Create a new instance of the UserRepository
        var userService = new UserService(userRepository); // Create a new instance of the UserService
        var user = new User // Create a new instance of the User
        {
            Name = "UserValid2",
            Email = "Exemple@mail.com",
            Password = "SecurePass123!"
        };

        // Act
        var response = userService.RegisterUser(user); // Register the user

        // Assert
        Assert.AreEqual("UserValid2", response.Name); // Check is the name is the same
        Assert.AreEqual("User registered successfully.", response.Message); // Checks if user is registered successfully
    }

    [TestMethod]
    public void RegisterUser_ShouldBeStoredInRepository() // Test to see if the user is stored in the repository
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
        userService.RegisterUser(user); // Register the user
        var users = userService.GetAllUsers(); // Get all users

        // Assert
        Assert.AreEqual(1, users.Count); // Check if there is only one user
        Assert.AreEqual("NewUser123", users[0].Name); // Check if the name is the same
        Assert.AreEqual("Mail@example.com", users[0].Email); // Check if the email is the same
    }
}
   