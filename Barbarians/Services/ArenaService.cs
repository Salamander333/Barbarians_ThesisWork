using Barbarians.Data;
using System.Threading.Tasks;

namespace Barbarians.Services
{
    public class ArenaService : IArenaService
    {
        private readonly ApplicationDbContext _db;

        public ArenaService(ApplicationDbContext db)
        {
            this._db = db;
        }

        public Task<string> AttackOpponent(string attackerName, string opponentName)
        {
            return null;
        }
    }
}
