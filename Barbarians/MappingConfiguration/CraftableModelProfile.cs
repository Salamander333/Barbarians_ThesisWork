using AutoMapper;
using Barbarians.Models;
using Barbarians.ViewModels.Craftables;

namespace Barbarians.MappingConfiguration
{
    public class CraftableModelProfile : Profile
    {
        public CraftableModelProfile()
        {
            CreateMap<CraftableArmor, CraftableArmorViewModel>();

            CreateMap<CraftableWeapon, CraftableWeaponViewModel>();
        }
    }
}
