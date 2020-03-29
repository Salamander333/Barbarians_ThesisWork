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
            foreach (var material in Enum.GetValues(typeof(Materials)))
            {
                var entity = new Material
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = material.ToString(),
                    Count = 0,
                    UserId = id,
                };

                _db.Materials.Add(entity);
            }

            _db.SaveChanges();
        }
    }
}
