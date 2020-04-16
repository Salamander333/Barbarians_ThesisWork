using Barbarians.Data;
using Barbarians.Data.GlobalEnums;
using Barbarians.Models;
using System;
using System.Linq;
using System.Text;
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

        public async Task<string> AttackOpponent(string attackerName, string opponentName)
        {
            //-------------TODO: Improve code quality!!!!!-------------------------------
            var result = new StringBuilder();

            var attackerId = _db.ApplicationUsers.Where(x => x.UserName == attackerName).FirstOrDefault().Id;
            var attackerCoins = _db.Materials.Where(s => s.UserId == attackerId).Where(x => x.Type == MaterialType.Currency).First().Count;
            var attackerHp = _db.ApplicationUsers.Where(x => x.UserName == attackerName).FirstOrDefault().Health;
            var attackerDamage = _db.Weapons.Where(s => s.UserId == attackerId && s.IsBroken == false).OrderByDescending(x => x.Damage).FirstOrDefault() == null ? 0 : _db.Weapons.Where(s => s.UserId == attackerId && s.IsBroken == false).OrderByDescending(x => x.Damage).FirstOrDefault().Damage;
            var attackerDefence = (_db.Armors.Where(s => s.UserId == attackerId && s.Type == ArmorTypes.Chest && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault() == null ? 0 : _db.Armors.Where(s => s.UserId == attackerId && s.Type == ArmorTypes.Chest && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault().Defence) +
                                  (_db.Armors.Where(s => s.UserId == attackerId && s.Type == ArmorTypes.Leggings && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault() == null ? 0 : _db.Armors.Where(s => s.UserId == attackerId && s.Type == ArmorTypes.Leggings && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault().Defence) +
                                  (_db.Armors.Where(s => s.UserId == attackerId && s.Type == ArmorTypes.Boots && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault() == null ? 0 : _db.Armors.Where(s => s.UserId == attackerId && s.Type == ArmorTypes.Boots && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault().Defence);


            var opponentId = _db.ApplicationUsers.Where(x => x.UserName == opponentName).FirstOrDefault().Id;
            var opponentCoins = _db.Materials.Where(s => s.UserId == opponentId).Where(x => x.Type == MaterialType.Currency).First().Count;
            var opponentHp = _db.ApplicationUsers.Where(x => x.UserName == opponentName).FirstOrDefault().Health;
            var opponentDamage = _db.Weapons.Where(s => s.UserId == opponentId && s.IsBroken == false).OrderByDescending(x => x.Damage).FirstOrDefault() == null ? 0 : _db.Weapons.Where(s => s.UserId == attackerId && s.IsBroken == false).OrderByDescending(x => x.Damage).FirstOrDefault().Damage;
            var opponentDefence = (_db.Armors.Where(s => s.UserId == opponentId && s.Type == ArmorTypes.Chest && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault() == null ? 0 : _db.Armors.Where(s => s.UserId == opponentId && s.Type == ArmorTypes.Chest && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault().Defence) +
                                  (_db.Armors.Where(s => s.UserId == opponentId && s.Type == ArmorTypes.Leggings && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault() == null ? 0 : _db.Armors.Where(s => s.UserId == opponentId && s.Type == ArmorTypes.Leggings && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault().Defence) +
                                  (_db.Armors.Where(s => s.UserId == opponentId && s.Type == ArmorTypes.Boots && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault() == null ? 0 : _db.Armors.Where(s => s.UserId == opponentId && s.Type == ArmorTypes.Boots && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault().Defence);

            for (int i = 0; i < 4; i++)
            {
                var damageTaken = 0;
                if (i % 2 == 0)
                {
                    damageTaken = attackerDamage - opponentDefence;
                    if (damageTaken <= 0)
                    {
                        damageTaken = 0;
                    }
                    opponentHp -= damageTaken;
                    result.Append($"{opponentName} got hit by {attackerName} for {damageTaken} damage.");
                    result.AppendLine();
                    if (_IsAttackedPlayerDead(opponentHp))
                    {
                        var reward = 0;

                        if (opponentCoins > 1)
                        {
                            reward = (int)MathF.Floor(opponentCoins / 2);

                        }

                        result.Append($"{opponentName} died.");
                        result.AppendLine();
                        result.Append($"{attackerName} took {reward} gold from {opponentName}.");
                        result.AppendLine();
                        await _FinalizeBattle(attackerId, opponentId, reward, attackerHp, opponentHp, "Attacker");
                        break;
                    }
                }
                else
                {
                    damageTaken = opponentDamage - attackerDefence;
                    if (damageTaken <= 0)
                    {
                        damageTaken = 0;
                    }
                    attackerHp -= damageTaken;
                    result.Append($"{attackerName} got hit by {opponentName} for {damageTaken} damage.");
                    result.AppendLine();
                    if (_IsAttackedPlayerDead(attackerHp))
                    {
                        result.Append($"{attackerName} died.");
                        result.AppendLine();
                        result.Append($"{opponentName} defended his glory and gold.");
                        result.AppendLine();
                        await _FinalizeBattle(opponentId, attackerId, 0, attackerHp, opponentHp, "Opponent");
                        break;
                    }
                    if (i == 3)
                    {
                        result.Append($"{opponentName} defended his glory and gold.");
                        result.AppendLine();
                        await _FinalizeBattle(opponentId, attackerId, 0, attackerHp, opponentHp, "Opponent");
                    }
                }
            }

            var report = new BattleReport
            {
                Id = Guid.NewGuid().ToString(),
                AttackerId = attackerId,
                OpponentId = opponentId,
                ReportString = result.ToString()
            };

            await _db.BattleReports.AddAsync(report);
            await _db.SaveChangesAsync();

            return report.Id;
        }

        private bool _IsAttackedPlayerDead(int hp)
        {
            if (hp <= 0)
            {
                return true;
            }

            return false;
        }

        private async Task _FinalizeBattle(string winnerId, string looserId, int reward, int attackerHp, int opponentHp, string winnerIs)
        {
            //-------------TODO: Improve code quality!!!!!-------------------------------
            var winnerCoins = _db.Materials
                .Where(x => x.Type == MaterialType.Currency && x.Name == "Coins" && x.UserId == winnerId)
                .FirstOrDefault();
            winnerCoins.Count += reward;

            var looserCoins = _db.Materials
                .Where(x => x.Type == MaterialType.Currency && x.Name == "Coins" && x.UserId == looserId)
                .FirstOrDefault();
            looserCoins.Count -= reward;

            var winnerWeapon = _db.Weapons.Where(s => s.UserId == winnerId && s.IsBroken == false).OrderByDescending(x => x.Damage).FirstOrDefault();
            var winnerChest = _db.Armors.Where(s => s.UserId == winnerId && s.Type == ArmorTypes.Chest && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault();
            var winnerLeggings = _db.Armors.Where(s => s.UserId == winnerId && s.Type == ArmorTypes.Leggings && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault();
            var winnerBoots = _db.Armors.Where(s => s.UserId == winnerId && s.Type == ArmorTypes.Boots && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault();
            if (winnerWeapon != null) winnerWeapon.IsBroken = true;
            if (winnerChest != null) winnerChest.IsBroken = true;
            if (winnerLeggings != null) winnerLeggings.IsBroken = true;
            if (winnerBoots != null) winnerBoots.IsBroken = true;

            var looserWeapon = _db.Weapons.Where(s => s.UserId == looserId && s.IsBroken == false).OrderByDescending(x => x.Damage).FirstOrDefault();
            var looserChest = _db.Armors.Where(s => s.UserId == looserId && s.Type == ArmorTypes.Chest && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault();
            var looserLeggings = _db.Armors.Where(s => s.UserId == looserId && s.Type == ArmorTypes.Leggings && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault();
            var looserBoots = _db.Armors.Where(s => s.UserId == looserId && s.Type == ArmorTypes.Boots && s.IsBroken == false).OrderByDescending(x => x.Defence).FirstOrDefault();
            if (looserWeapon != null) looserWeapon.IsBroken = true;
            if (looserChest != null) looserChest.IsBroken = true;
            if (looserLeggings != null) looserLeggings.IsBroken = true;
            if (looserBoots != null) looserBoots.IsBroken = true;

            var winnerHp = _db.ApplicationUsers.Where(x => x.Id == winnerId).FirstOrDefault();
            var looserHp = _db.ApplicationUsers.Where(x => x.Id == looserId).FirstOrDefault();

            if (winnerIs == "Attacker")
            {
                winnerHp.Health = attackerHp;
                if (opponentHp < 1)
                {
                    looserHp.Health = 1;
                }
                else
                {
                    looserHp.Health = opponentHp;
                }
            }
            else if (winnerIs == "Opponent")
            {
                winnerHp.Health = opponentHp;
                if (attackerHp < 1)
                {
                    looserHp.Health = 1;
                }
                else
                {
                    looserHp.Health = attackerHp;
                }
            }

            await _db.SaveChangesAsync();
        }
    }
}
