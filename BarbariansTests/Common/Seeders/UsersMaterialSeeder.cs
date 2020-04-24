using Barbarians.Data;
using Barbarians.Data.GlobalEnums;
using Barbarians.Models;
using System;
using System.Threading.Tasks;

namespace BarbariansTests.Common.Seeders
{
    public class UsersMaterialSeeder
    {
        public async Task SeedUserWithMaterials(ApplicationDbContext context, string id)
        {
            foreach (var material in MaterialList.MaterialObjects)
            {
                var entity = new Material
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = material.Name.ToString(),
                    Type = material.Type,
                    Tier = material.Tier,
                    Count = 10,
                    UserId = id,
                };

                await context.Materials.AddAsync(entity);
            }

            await context.SaveChangesAsync();
        }
    }
}
