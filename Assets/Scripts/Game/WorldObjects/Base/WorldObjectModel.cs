using System;
using Framework.Extensions;
using Game.Core.Data;
using Game.Core.Resources;
using Game.WorldObjects.Base.Configuration;
using UnityEngine;

namespace Game.WorldObjects.Base
{
    public abstract class WorldObjectModel<T> where T : IWorldObjectConfiguration<IWorldObjectSettings>
    {
        protected IGameData GameData;
        protected T Configuration;

        public event Action StateChanged;

        public int Level { get; private set; }
        public bool IsOpened { get; private set; }
        public bool IsMaxed { get; private set; }
        public bool IsManagerAssigned { get; private set; }
        public int IdleMining { get; private set; }
        public int UpgradeCost { get; private set; }
        public int ManagerCost { get; private set; }
        public IWorldObjectSettings Settings { get; private set; }

        public virtual void Initialize(IGameData gameData, T configuration)
        {
            GameData = gameData;
            Configuration = configuration;

            var levelData = GameData.GetWorldObjectLevelData(Configuration.Id);
            Level = levelData.Level;
            IsManagerAssigned = levelData.IsManagerAssigned;
            ManagerCost = Configuration.ManagerCost;

            if (Level > 0)
            {
                IsOpened = true;
                Settings = Configuration.GetSettings(Level);
            }
            else
            {
                Level = 1;
                IsManagerAssigned = false;

                Settings = Configuration.GetSettings(Level);
                if (Settings.UpgradeCost == 0)
                {
                    IsOpened = true;
                }
                else
                {
                    Level = 0;
                    IsOpened = false;
                }
            }

            IdleMining = CalculateIdleMining();

            var nextSettings = Configuration.GetSettings(Level + 1);
            if (nextSettings != null)
            {
                IsMaxed = false;
                UpgradeCost = nextSettings.UpgradeCost;
            }
            else
            {
                IsMaxed = true;
            }

            SaveState(Configuration.Id, Level, IsManagerAssigned, IdleMining);
        }

        public virtual void Update()
        {
        }

        public void Upgrade()
        {
            if (IsMaxed)
            {
                return;
            }

            GameData.GetResource(ResourceType.Gold).Decrease(UpgradeCost);

            Level++;
            IsOpened = true;
            Settings = Configuration.GetSettings(Level);

            var nextSettings = Configuration.GetSettings(Level + 1);
            if (nextSettings != null)
            {
                IsMaxed = false;
                UpgradeCost = nextSettings.UpgradeCost;
            }
            else
            {
                IsMaxed = true;
            }

            IdleMining = CalculateIdleMining();
            SaveState(Configuration.Id, Level, IsManagerAssigned, IdleMining);
        }

        public void AssignManager(bool isManagerAssigned)
        {
            GameData.GetResource(ResourceType.Gold).Decrease(ManagerCost);

            IsManagerAssigned = isManagerAssigned;
            IdleMining = CalculateIdleMining();
            SaveState(Configuration.Id, Level, IsManagerAssigned, IdleMining);
        }

        private int CalculateIdleMining()
        {
            if (IsManagerAssigned)
            {
                var time = Settings.MoveTime * 2 + Settings.WorkTime;
                return Mathf.FloorToInt(Settings.Load / time) * Settings.Units;
            }

            return 0;
        }

        private void SaveState(string configurationId, int level, bool isManagerAssigned, int idleMining)
        {
            Debug.Log(string.Format("{0}: id - {1}, level - {2}, manager - {3}", GetType().Name, configurationId, level,
                isManagerAssigned));

            GameData.SetWorldObjectLevelData(configurationId, new WorldObjectLevelData
            {
                Id = configurationId,
                Level = level,
                IsManagerAssigned = isManagerAssigned,
                IdleMining = idleMining
            });

            StateChanged.SafeInvoke();
        }
    }
}