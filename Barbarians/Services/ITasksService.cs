using System.Threading.Tasks;

namespace Barbarians.Services
{
    public interface ITasksService
    {
        public bool HasActiveTask(string id, string type);

        public bool IsGatheringTaskValid(string material, string difficulty);

        public Task GenerateGatheringTask(string material, string difficulty, string userId);

        public Task<bool> CheckGatheringTaskCompletion(string userId);
    }
}
