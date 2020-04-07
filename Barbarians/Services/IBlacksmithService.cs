using Barbarians.ViewModels.Craftables;
using System.Threading.Tasks;

namespace Barbarians.Services
{
    public interface IBlacksmithService
    {
        public CraftableModel CreateCraftableModel(string craft, string userId);

        public Task AddArmorItemToUser(string id, string userId);

        public Task AddWeaponItemToUser(string id, string userId);
    }
}
