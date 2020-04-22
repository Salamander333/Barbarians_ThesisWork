using Barbarians.Data;
using Barbarians.Services;
using BarbariansTests.Common;
using BarbariansTests.Common.Seeders;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BarbariansTests.Services
{
    public class UsersServiceTests
    {
        [Fact]
        public async Task UserSeedsCorrectAmountOfMaterialsOnRegister()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var userService = this.GetUserService(context);

            var seeder = new UsersSeeder();
            await seeder.SeedUsers(context);

            var user = context.Users.First(x => x.UserName == "User1");

            await userService.SeedDatabaseOnSuccessfulRegister(user.Id);

            var result = context.Materials.Where(x => x.UserId == user.Id).Count();

            Assert.True(result == 10, $"User has incorrect amount of rescources. {result}");
        }

        private UsersService GetUserService(ApplicationDbContext context)
        {
            var userService = new UsersService(context);

            return userService;
        }
    }
}
