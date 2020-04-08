using Barbarians.Data;
using System;
using System.Threading.Tasks;
using Barbarians.Models;
using Barbarians.Data.GlobalEnums;

namespace Barbarians.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _db;

        public UsersService(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task SeedDatabaseOnSuccessfulRegister(string id)
        {
            foreach (var material in MaterialList.MaterialObjects)
            {
                var entity = new Material
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = material.Name.ToString(),
                    Type = material.Type,
                    Tier = material.Tier,
                    Count = 0,
                    UserId = id,
                };

                await _db.Materials.AddAsync(entity);
            }

           await _db.SaveChangesAsync();
        }
    }
}
