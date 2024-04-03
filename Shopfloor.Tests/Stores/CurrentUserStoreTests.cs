using FluentAssertions;
using NSubstitute;
using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Stores;

namespace Shopfloor.Tests;

public class CurrentUserStoreTests
{
    private readonly CurrentUserStore _sut;
    private readonly IProvider<Role> _roleProvider = Substitute.For<IProvider<Role>>();
    private readonly IRoleUserProvider _roleUserProvider = Substitute.For<IRoleUserProvider>();
    private readonly INotifier _notifier = Substitute.For<INotifier>();
    public CurrentUserStoreTests()
    {
        _sut = new(_roleProvider, _roleUserProvider, _notifier);
    }
    [Fact]
    public void HasRole_ShouldReturnTrue_WhenUserHasRoleByValue()
    {
        Role role = new(1, "test", 5);
        RoleUser roleUser = new()
        {
            RoleId = 1,
            UserId = 1
        };
        IEnumerable<Role> roles = [role];
        IEnumerable<RoleUser> roleUsers = [roleUser];

        _roleProvider.GetAll().Returns(roles);
        _roleUserProvider.GetAllForUser(1).Returns(roleUsers);

        bool result = _sut.HasRole(role.Value);

        result.Should().BeTrue();
    }
    [Fact]
    public void HasRole_ShouldReturnFalse_WhenUserHasNoRoleByValue()
    {
        Role role = new(1, "test", 5);
        IEnumerable<Role> roles = [role];
        IEnumerable<RoleUser> roleUsers = [];

        _roleProvider.GetAll().Returns(roles);
        _roleUserProvider.GetAllForUser(1).Returns(roleUsers);

        bool result = _sut.HasRole(role.Value);

        result.Should().BeFalse();
    }
    [Fact]
    public void HasRole_ShouldReturnTrue_WhenUserHasRoleByRole()
    {
        Role role = new(1, "test", 5);
        RoleUser roleUser = new()
        {
            RoleId = 1,
            UserId = 1
        };
        IEnumerable<Role> roles = [role];
        IEnumerable<RoleUser> roleUsers = [roleUser];

        _roleProvider.GetAll().Returns(roles);
        _roleUserProvider.GetAllForUser(1).Returns(roleUsers);

        bool result = _sut.HasRole(role);
        result.Should().BeTrue();
    }
    [Fact]
    public void HasRole_ShouldReturnFalse_WhenUserHasNoRoleByRole()
    {
        Role role = new(1, "test", 5);

        IEnumerable<Role> roles = [role];
        IEnumerable<RoleUser> roleUsers = [];

        _roleProvider.GetAll().Returns(roles);
        _roleUserProvider.GetAllForUser(1).Returns(roleUsers);

        bool result = _sut.HasRole(role);
        result.Should().BeFalse();
    }
}