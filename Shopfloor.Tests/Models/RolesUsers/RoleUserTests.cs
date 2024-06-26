﻿using FluentAssertions;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.UserModel;

namespace Shopfloor.Tests.Models.RolesUsers
{
    public sealed class RoleUserTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        public void RoleId_WhenRoleHasId_ShouldReturnRoleId(int id)
        {
            // Arrange
            Role role = new(id, "test", 1);
            User user = new()
            {
                Id = 10,
                Username = "test"
            };
            RoleUser roleUser = new()
            {
                RoleId = (int)role.Id!,
                UserId = (int)user.Id!
            };
            // Act
            int? result = roleUser.RoleId;
            // Assert
            result.Should().Be(id);
            result.Should().NotBeNull();
        }
        // [Fact]
        // public void RoleId_WhenRoleDoesntHaveId_ShouldReturnNull()
        // {
        //     // Arrange
        //     Role role = new("test", 1);
        //     User user = new(10, "test", "test", "test", "test", true);
        //     RoleUser roleUser = new()
        //     {
        //         RoleId = (int)role.Id!,
        //         UserId = (int)user.Id!
        //     };
        //     // Act
        //     int? result = roleUser.RoleId;
        //     // Assert
        //     result.Should().BeNull();
        //     result.Should().NotBe(10);
        // }
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        public void UsereId_WhenUserHasId_ShouldReturnRoleId(int id)
        {
            // Arrange
            Role role = new(10, "test", 1);
            User user = new()
            {
                Id = id,
                Username = "test"
            };
            RoleUser roleUser = new()
            {
                RoleId = (int)role.Id!,
                UserId = (int)user.Id!
            };
            // Act
            int? result = roleUser.UserId;
            // Assert
            result.Should().Be(id);
            result.Should().NotBeNull();
        }
        // [Fact]
        // public void UsereId_WhenUserDoesntHaveId_ShouldReturnNull()
        // {
        //     // Arrange
        //     Role role = new(3, "test", 1);
        //     User user = new("test", "test", "test", "test", true);
        //     RoleUser roleUser = new()
        //     {
        //         RoleId = (int)role.Id!,
        //         UserId = (int)user.Id!
        //     };
        //     // Act
        //     int? result = roleUser.UserId;
        //     // Assert
        //     result.Should().BeNull();
        //     result.Should().NotBe(3);
        // }
    }
}