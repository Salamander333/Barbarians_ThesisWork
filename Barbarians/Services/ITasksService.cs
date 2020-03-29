namespace Barbarians.Services
{
    public interface ITasksService
    {
        public bool HasActiveTasksOfType(string id, string type);

        public bool IsActiveTaskComplete(string id, string type);

        public bool IsGatheringTaskValid(string material, string difficulty);
    }
}
