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
    [Fact]
    public void UserStoreTest()
    {
        IProvider<Role> roleProvider = Substitute.For<IProvider<Role>>();
        IRoleUserProvider roleUserProvider = Substitute.For<IRoleUserProvider>();
        INotifier notifier = Substitute.For<INotifier>();

        CurrentUserStore currentUserStore = new(roleProvider, roleUserProvider, notifier);

        bool result = true;
        result.Should().BeTrue();
    }
}
