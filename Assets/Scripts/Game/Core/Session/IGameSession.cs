using System;
using Game.Core.Data;

namespace Game.Core.Session
{
    public interface IGameSession
    {
        event Action<IGameData> Initialized;
        bool IsInitialized { get; }
        IGameData Data { get; }
        void Save();
    }
}