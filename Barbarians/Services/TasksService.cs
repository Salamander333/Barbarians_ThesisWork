using System;
using System.Linq;
using System.Threading.Tasks;
using Barbarians.Data;
using Barbarians.Data.GlobalEnums;
using Barbarians.Models;

namespace Barbarians.Services
{
    public class TasksService : ITasksService
    {
        private readonly ApplicationDbContext _db;

        public TasksService(ApplicationDbContext db)
        {
            this._db = db;
        }

        public bool HasActiveTask(string id, string type)
        {
            var result = false;

            if (type == "Gather")
            {
                result = _db.TasksGather.Any(x => x.UserId == id && x.IsComplete == false);
            }
            
            return result;
        }

        public bool IsGatheringTaskValid(string material, string difficulty)
        {
            if (Enum.IsDefined(typeof(MaterialType), material) && Enum.IsDefined(typeof(AdventureDifficulties), difficulty))
            {
                return true;
            }

            return false;
        }

        public async Task GenerateGatheringTask(string material, string difficulty, string userId)
        {
            var adventure = _GetGatherTaskRewards((MaterialType)Enum.Parse(typeof(MaterialType), material),
                                                  (AdventureDifficulties)Enum.Parse(typeof(AdventureDifficulties), difficulty));

            var task = new TaskGather
            {
                Id = Guid.NewGuid().ToString(),
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddSeconds(5),
                Rescource = (Materials)Enum.Parse(typeof(Materials), adventure.MaterialType, true),
                Count = adventure.MaterialCount,
                GoldIncome = adventure.CoinCount,
                IsComplete = false,
                UserId = userId
            };

            await _db.TasksGather.AddAsync(task);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> CheckGatheringTaskCompletion(string userId)
        {

            if (this.HasActiveTask(userId, "Gather"))
            {
                var isComplete = this._IsActiveTaskComplete(userId, "Gather");
                if (await isComplete)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private async Task<bool> _IsActiveTaskComplete(string id, string type)
        {
            var task = _db.TasksGather.FirstOrDefault(x => x.UserId == id && x.IsComplete == false);
            var userRescources = _db.Materials.Where(x => x.UserId == id);

            if (DateTime.UtcNow > task.EndTime)
            {
                task.IsComplete = true;
                var rescource = userRescources.Where(x => x.Name == task.Rescource.ToString()).FirstOrDefault();
                if (rescource != null) { rescource.Count += task.Count; }
                var coins = userRescources.Where(x => x.Name == "Coins").FirstOrDefault();
                if (coins != null) { coins.Count += task.GoldIncome; }
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        private Rescource _GetGatherTaskRewards(MaterialType material, AdventureDifficulties difficulty)
        {
            var result = new Rescource
            {
                MaterialType = "",
                MaterialCount = new Random().Next(1, 3),
                CoinCount = 0,
            };

            switch (material)
            {
                case MaterialType.Wood:
                    switch (difficulty)
                    {
                        case AdventureDifficulties.Easy:
                            result.MaterialType = "Spruce";
                            result.CoinCount = new Random().Next(5, 10);
                            break;
                        case AdventureDifficulties.Medium:
                            result.MaterialType = "Oak";
                            result.CoinCount = new Random().Next(15, 20);
                            break;
                        case AdventureDifficulties.Hard:
                            result.MaterialType = "Ash";
                            result.CoinCount = 30;
                            break;
                        default:
                            result.MaterialType = "Spruce";
                            result.CoinCount = new Random().Next(5, 10);
                            break;
                    }
                    break;

                case MaterialType.Metal:
                    switch (difficulty)
                    {
                        case AdventureDifficulties.Easy:
                            result.MaterialType = "Copper";
                            result.CoinCount = new Random().Next(5, 10);
                            break;
                        case AdventureDifficulties.Medium:
                            result.MaterialType = "Iron";
                            result.CoinCount = new Random().Next(15, 20);
                            break;
                        case AdventureDifficulties.Hard:
                            result.MaterialType = "Mithril";
                            result.CoinCount = 30;
                            break;
                        default:
                            result.MaterialType = "Copper";
                            result.CoinCount = new Random().Next(5, 10);
                            break;
                    }
                    break;

                case MaterialType.Cloth:
                    switch (difficulty)
                    {
                        case AdventureDifficulties.Easy:
                            result.MaterialType = "Silk";
                            result.CoinCount = new Random().Next(5, 10);
                            break;
                        case AdventureDifficulties.Medium:
                            result.MaterialType = "Linen";
                            result.CoinCount = new Random().Next(15, 20);
                            break;
                        case AdventureDifficulties.Hard:
                            result.MaterialType = "Jute";
                            result.CoinCount = 30;
                            break;
                        default:
                            result.MaterialType = "Silk";
                            result.CoinCount = new Random().Next(5, 10);
                            break;
                    }
                    break;

                default:
                    result.MaterialType = "Spruce";
                    result.CoinCount = new Random().Next(5, 10);
                    break;
            }

            return result;
        }
    }

    class Rescource
    {
        public string MaterialType { get; set; }

        public int MaterialCount { get; set; }

        public int CoinCount { get; set; }
    }
}
