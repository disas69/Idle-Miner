using System;
using Framework.Extensions;
using Game.Core.Data;
using Game.Core.Resources;
using UnityEngine;

namespace Game.Core.Session
{
    public class GameSession : IGameSession
    {
        private readonly DataKeeper _dataKeeper;

        public event Action<IGameData> Initialized;
        public bool IsInitialized { get; private set; }

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
            var lastSessionData = _dataKeeper.Data.GetLastSessionTimeData();
            if (lastSessionData.IsCaptured())
            {
                var difference = Mathf.FloorToInt((float)(DateTime.Now - lastSessionData.GetDateTime()).TotalSeconds);
                if (difference > 0)
                {
                    var idleGoldAmount = _dataKeeper.Data.TotalIdleMining * difference;
                    Data.GetResource(ResourceType.Gold).Increase(idleGoldAmount);
                    Debug.Log(string.Format("Idle Gold amount: {0}", idleGoldAmount));
                }
            }

            IsInitialized = true;
            Initialized.SafeInvoke(Data);
        }

        public void Save()
        {
            _dataKeeper.Save();
        }
    }
}