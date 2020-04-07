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
                result = _db.TasksGather.Any(x => x.IsComplete == false);
            }
            
            return result;
        }

        public async Task<bool> IsActiveTaskComplete(string id, string type)
        {
            var task = _db.TasksGather.FirstOrDefault(x => x.IsComplete == false);
            var userRescources = _db.Materials.Where(x => x.UserId == id);

            if (DateTime.UtcNow > task.EndTime)
            {
                task.IsComplete = true;
                var rescource = userRescources.Where(x => x.Name == task.Rescource.ToString()).FirstOrDefault();
                rescource.Count += task.Count;
                var coins = userRescources.Where(x => x.Name == "Coins").FirstOrDefault();
                coins.Count += task.GoldIncome;

                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public bool IsGatheringTaskValid(string material, string difficulty)
        {
            if (material == "Wood" || material == "Metal" || material == "Cloth")
            {
                return true;
            }

            if (difficulty == "Easy" || difficulty == "Medium" || difficulty == "Hard")
            {
                return true;
            }

            return false;
        }

        public async Task GenerateGatheringTask(string material, string difficulty, string userId)
        {
            var adventure = _GetGatherTaskRewards(material, difficulty);

            var task = new TaskGather
            {
                Id = Guid.NewGuid().ToString(),
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddSeconds(30),
                Rescource = (Materials)Enum.Parse(typeof(Materials), adventure.MaterialType, true),
                Count = adventure.MaterialCount,
                GoldIncome = adventure.CoinCount,
                IsComplete = false,
                UserId = userId
            };

            await _db.TasksGather.AddAsync(task);
            await _db.SaveChangesAsync();
        }

        private Rescource _GetGatherTaskRewards(string material, string difficulty)
        {
            var result = new Rescource
            {
                MaterialType = "",
                MaterialCount = new Random().Next(1, 3),
                CoinCount = 0,
            };

            switch (material)
            {
                case "Wood":
                    switch (difficulty)
                    {
                        case "Easy":
                            result.MaterialType = "Spruce";
                            result.CoinCount = new Random().Next(5, 10);
                            break;
                        case "Medium":
                            result.MaterialType = "Oak";
                            result.CoinCount = new Random().Next(15, 20);
                            break;
                        case "Hard":
                            result.MaterialType = "Ash";
                            result.CoinCount = 30;
                            break;
                        default:
                            result.MaterialType = "Spruce";
                            result.CoinCount = new Random().Next(5, 10);
                            break;
                    }
                    break;

                case "Metal":
                    switch (difficulty)
                    {
                        case "Easy":
                            result.MaterialType = "Copper";
                            result.CoinCount = new Random().Next(5, 10);
                            break;
                        case "Medium":
                            result.MaterialType = "Iron";
                            result.CoinCount = new Random().Next(15, 20);
                            break;
                        case "Hard":
                            result.MaterialType = "Mithril";
                            result.CoinCount = 30;
                            break;
                        default:
                            result.MaterialType = "Copper";
                            result.CoinCount = new Random().Next(5, 10);
                            break;
                    }
                    break;

                case "Cloth":
                    switch (difficulty)
                    {
                        case "Easy":
                            result.MaterialType = "Linen";
                            result.CoinCount = new Random().Next(5, 10);
                            break;
                        case "Medium":
                            result.MaterialType = "Jute";
                            result.CoinCount = new Random().Next(15, 20);
                            break;
                        case "Hard":
                            result.MaterialType = "Silk";
                            result.CoinCount = 30;
                            break;
                        default:
                            result.MaterialType = "Linen";
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

        public async Task<bool> CheckGatheringTaskCompletion(string userId)
        {

            if (this.HasActiveTask(userId, "Gather"))
            {
                var isComplete = this.IsActiveTaskComplete(userId, "Gather");
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
    }

    class Rescource
    {
        public string MaterialType { get; set; }

        public int MaterialCount { get; set; }

        public int CoinCount { get; set; }
    }
}
