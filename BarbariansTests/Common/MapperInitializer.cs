using AutoMapper;
using Barbarians.Models;
using Barbarians.ViewModels.Craftables;

namespace BarbariansTests.Common
{
    public class MapperInitializer
    {
        public static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(opt =>
            {
                opt.CreateMap<CraftableArmor, CraftableArmorViewModel>();
                opt.CreateMap<CraftableWeapon, CraftableWeaponViewModel>();
            });

            var mapper = config.CreateMapper();

            return mapper;
        }
    }
}
