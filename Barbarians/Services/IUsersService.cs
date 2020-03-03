using System.Threading.Tasks;

namespace Barbarians.Services
{
    public interface IUsersService
    {
        public Task SeedDatabaseOnSuccessfulRegister(string id);
    }
}
