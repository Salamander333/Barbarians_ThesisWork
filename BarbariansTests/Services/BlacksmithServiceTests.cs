using Barbarians.Data;
using Barbarians.Models;
using Barbarians.Services;
using BarbariansTests.Common;
using BarbariansTests.Common.Seeders;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BarbariansTests.Services
{
    public class BlacksmithServiceTests
    {
        [Fact]
        public async Task DoesAddWeaponItemToUserMethodAddItem()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var service = GetBlacksmithService(context);

            var usersSeeder = new UsersSeeder();
            await usersSeeder.SeedUsers(context);

            var itemsSeeder = new CraftableWeaponsSeder();
            await itemsSeeder.SeedCraftableWeapons(context);

            await service.AddWeaponItemToUser("Id1", "Id1");

            var result = context.Weapons.Where(x => x.Name == "Test weapon 1" && x.UserId == "Id1");

            Assert.True(result != null, "User doesn't have added weapon");
        }

        [Fact]
        public async Task DoesAddArmorItemToUserMethodAddItem()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var service = GetBlacksmithService(context);

            var usersSeeder = new UsersSeeder();
            await usersSeeder.SeedUsers(context);

            var itemsSeeder = new CraftableArmorsSeeder();
            await itemsSeeder.SeedCraftableArmors(context);

            await service.AddArmorItemToUser("Id1", "Id1");

            var result = context.Armors.Where(x => x.Name == "Test armor 1" && x.UserId == "Id1");

            Assert.True(result != null, "User doesn't have added armor");
        }

        private BlacksmithService GetBlacksmithService(ApplicationDbContext context)
        {
            var mapper = MapperInitializer.InitializeMapper();
            var userManager = this.GetUserManagerMock(context);
            var blacksmithService = new BlacksmithService(context, userManager.Object, mapper);

            return blacksmithService;
        }

        private Mock<UserManager<ApplicationUser>> GetUserManagerMock(ApplicationDbContext context)
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock
                .Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()))
                .Callback((ApplicationUser user) =>
                {
                    context.Update(user);
                })
                .ReturnsAsync(IdentityResult.Success);

            return userManagerMock;
        }
    }
}
