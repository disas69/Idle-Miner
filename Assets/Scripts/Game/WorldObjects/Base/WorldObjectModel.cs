using System;
using Extensions;
using Game.Core;
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

            GameContext.ResourceManager.Decrease(ResourceType.Gold, UpgradeCost);

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

            UnityEngine.Debug.Log(string.Format("{0} upgrade: Level -> {1}, Units -> {2} Load -> {3}, Move Time -> {4}, Work Time -> {5}",
                GetType().Name, Level, Settings.Units, Settings.Load, Settings.MoveTime, Settings.WorkTime));
        }

        public void AssignManager(bool isManagerAssigned)
        {
            if (isManagerAssigned)
            {
                GameContext.ResourceManager.Decrease(ResourceType.Gold, ManagerCost);
            }

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