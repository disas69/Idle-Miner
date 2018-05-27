using Game.Core.Data;

namespace Game.WorldObjects.Base
{
    public interface IWorldObject
    {
        void Initialize(IGameData gameData);
    }
}