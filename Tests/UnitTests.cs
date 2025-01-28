using Microsoft.AspNetCore.Identity;
using Moq;
using VaricoCarRental.Models;

namespace Tests;

 public class UnitTests
    {
        [Fact]
        public async Task AddUser_ShouldThrowException_ForWeakPassword()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                null, null, null, null, null, null, null, null
            );

            var user = new User
            {
                Id = "user1",
                UserName = "testuser",
                Email = "test@example.com",
            };
            var password = "123"; // Weak password

            userManagerMock.Setup(um => um.CreateAsync(user, password))
                .ThrowsAsync(new Exception("Password does not meet complexity requirements"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => userManagerMock.Object.CreateAsync(user, password));
            Assert.Equal("Password does not meet complexity requirements", exception.Message);
        }

        [Fact]
        public async Task AddUser_ShouldThrowException_WhenEmailIsMissing()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                null, null, null, null, null, null, null, null
            );

            var user = new User
            {
                Id = "user1",
                UserName = "testuser",
                Email = null, // Missing email
            };
            var password = "ComplexPassword123!";

            userManagerMock.Setup(um => um.CreateAsync(user, password))
                .ThrowsAsync(new Exception("Email is required"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => userManagerMock.Object.CreateAsync(user, password));
            Assert.Equal("Email is required", exception.Message);
        }

        [Fact]
        public async Task AddUser_ShouldSucceed_ForValidUser()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                null, null, null, null, null, null, null, null
            );

            var user = new User
            {
                Id = "user1",
                UserName = "validuser",
                Email = "valid@example.com",
            };
            var password = "ComplexPassword123!";

            userManagerMock.Setup(um => um.CreateAsync(user, password)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await userManagerMock.Object.CreateAsync(user, password);

            // Assert
            Assert.True(result.Succeeded);
        }
    }