using System.Threading.Tasks;

namespace Barbarians.Services
{
    public interface IArenaService
    {
        public Task<string> AttackOpponent(string attackerName, string opponentName);
    }
}
