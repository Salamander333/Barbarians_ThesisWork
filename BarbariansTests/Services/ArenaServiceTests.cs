using Barbarians.Data;
using Barbarians.Services;
using BarbariansTests.Common;
using BarbariansTests.Common.Seeders;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BarbariansTests.Services
{
    public class ArenaServiceTests
    {
        [Fact]
        public async Task DefendantShouldKillAttacker()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var arenaService = this.GetArenaService(context);

            var seederUsers = new UsersSeeder();
            await seederUsers.SeedUsers(context);
            var seederCoins = new UsersCoinsSeeder();
            await seederCoins.SeedUsersWithCoins(context);
            var arenaSeeder = new ArenaSeeder();
            await arenaSeeder.SeedDefendantWithKillingWeapon(context);

            var battleReportId = await arenaService.AttackOpponent("User1", "User2");
            var battleResult = context.BattleReports.Where(x => x.Id == battleReportId).SingleOrDefault().ReportString;

            var result = battleResult.Split(".\r\n");

            Assert.True(result[result.Count() - 2] == "User2 defended his glory and gold", $"Defendant lives, but still looses.");
            Assert.True(result[result.Count() - 3] == "User1 died", $"Attacker doen't die when hit with killing blow.");
        }

        [Fact]
        public async Task AttackerShouldWinWhenOpponentIsKilledAndShouldTakeHalfHisGold()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var arenaService = this.GetArenaService(context);

            var seederUsers = new UsersSeeder();
            await seederUsers.SeedUsers(context);
            var seederCoins = new UsersCoinsSeeder();
            await seederCoins.SeedUsersWithCoins(context);
            var arenaSeeder = new ArenaSeeder();
            await arenaSeeder.SeedAttackerWithKillingWeapon(context);

            var battleReportId = await arenaService.AttackOpponent("User1", "User2");
            var battleResult = context.BattleReports.Where(x => x.Id == battleReportId).SingleOrDefault().ReportString;

            var result = battleResult.Split(".\r\n");

            Assert.True(result[result.Count() - 2] == "User1 took 250 gold from User2", $"Defendant dies, but still ties.");
        }

        [Fact]
        public async Task OpponendShouldWinWhenBothPlayersHaveNoItems_Tie()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var arenaService = this.GetArenaService(context);

            var seederUsers = new UsersSeeder();
            await seederUsers.SeedUsers(context);
            var seederCoins = new UsersCoinsSeeder();
            await seederCoins.SeedUsersWithCoins(context);

            var battleReportId = await arenaService.AttackOpponent("User1", "User2");
            var battleResult = context.BattleReports.Where(x => x.Id == battleReportId).SingleOrDefault().ReportString;

            var result = battleResult.Split(".\r\n");

            Assert.True(result[result.Count() - 2] == "User2 defended his glory and gold", $"Defendant doen't die, but still lost.");
        }

        private ArenaService GetArenaService(ApplicationDbContext context)
        {
            var arenaService = new ArenaService(context);

            return arenaService;
        }
    }
}
