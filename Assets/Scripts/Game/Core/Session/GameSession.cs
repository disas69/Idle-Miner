using System;
using Extensions;
using Game.Core.Data;
using Game.Core.Resources;
using UnityEngine;

namespace Game.Core.Session
{
    public class GameSession : IGameSession
    {
        private readonly DataKeeper _dataKeeper;

        public event Action<IGameData> Initialized;
        public int IdleMiningResul { get; private set; }

        public IGameData Data
        {
            get { return _dataKeeper.Data; }
        }

        public GameSession()
        {
            _dataKeeper = new DataKeeper();
            _dataKeeper.Load();

            Initialize();
        }

        private void Initialize()
        {
            //TODO: Get the right date using NTP synchronization in order to prevent cheating
            var lastSessionData = _dataKeeper.Data.GetLastSessionTimeData();
            if (lastSessionData.IsCaptured())
            {
                var difference = Mathf.FloorToInt((float) (DateTime.Now - lastSessionData.GetDateTime()).TotalSeconds);
                if (difference > 0)
                {
                    IdleMiningResul = _dataKeeper.Data.TotalIdleMining * difference;
                    Data.GetResource(ResourceType.Gold).Increase(IdleMiningResul);
                }
            }

            Initialized.SafeInvoke(Data);
        }

        public void Save()
        {
            _dataKeeper.Save();
        }
    }
}