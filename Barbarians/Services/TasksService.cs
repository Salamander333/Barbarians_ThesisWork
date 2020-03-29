using Barbarians.Data;

namespace Barbarians.Services
{
    public class TasksService : ITasksService
    {
        private readonly ApplicationDbContext _db;

        public TasksService(ApplicationDbContext db)
        {
            this._db = db;
        }

        public bool HasActiveTasksOfType(string id, string type)
        {
            throw new System.NotImplementedException();
        }

        public bool IsActiveTaskComplete(string id, string type)
        {
            throw new System.NotImplementedException();
        }

        public bool IsGatheringTaskValid(string material, string difficulty)
        {
            if (material == "wood" || material == "metal" || material == "cloth")
            {
                return true;
            }

            if (difficulty == "easy" || difficulty == "medium" || difficulty == "hard")
            {
                return true;
            }

            return false;
        }
    }
}
