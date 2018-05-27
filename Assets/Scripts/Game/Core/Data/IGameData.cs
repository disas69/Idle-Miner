using Game.Core.Resources;

namespace Game.Core.Data
{
    public interface IGameData
    {
        int TotalIdleMining { get; }
        WorldObjectLevelData GetWorldObjectLevelData(string configurationId);
        void SetWorldObjectLevelData(string configurationId, WorldObjectLevelData levelData);
        IResource GetResource(ResourceType type);
        TimeData GetLastSessionTimeData();
    }
}