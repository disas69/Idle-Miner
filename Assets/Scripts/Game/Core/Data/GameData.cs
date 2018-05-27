using System;
using System.Collections.Generic;
using Game.Core.Resources;
using Game.Core.Resources.Currency;
using UnityEngine;

namespace Game.Core.Data
{
    [Serializable]
    public class GameData : IGameData
    {
        [SerializeField] private GoldResource _gold = new GoldResource(100);
        [SerializeField] private GemsResource _gems = new GemsResource();
        [SerializeField] private TimeData _lastSessionTimeData = new TimeData();
        [SerializeField] private List<WorldObjectLevelData> _worldObjectLevels = new List<WorldObjectLevelData>();
        [SerializeField] private int _totalIdleMining;

        public int TotalIdleMining
        {
            get { return _totalIdleMining; }
        }

        public WorldObjectLevelData GetWorldObjectLevelData(string configurationId)
        {
            var levelData = _worldObjectLevels.Find(w => w.Id == configurationId);
            if (levelData != null)
            {
                return levelData;
            }

            levelData = new WorldObjectLevelData
            {
                Id = configurationId,
                Level = 0,
                IsManagerAssigned = false
            };

            _worldObjectLevels.Add(levelData);
            return levelData;
        }

        public void SetWorldObjectLevelData(string configurationId, WorldObjectLevelData newLevelData)
        {
            var levelData = _worldObjectLevels.Find(w => w.Id == configurationId);
            if (levelData != null)
            {
                levelData.Level = newLevelData.Level;
                levelData.IsManagerAssigned = newLevelData.IsManagerAssigned;
                levelData.IdleMining = newLevelData.IdleMining;
            }
            else
            {
                levelData = new WorldObjectLevelData
                {
                    Id = configurationId,
                    Level = newLevelData.Level,
                    IsManagerAssigned = newLevelData.IsManagerAssigned,
                    IdleMining = newLevelData.IdleMining
                };

                _worldObjectLevels.Add(levelData);
            }

            _totalIdleMining = CalculateTotalIdleMining();
        }

        public IResource GetResource(ResourceType type)
        {
            switch (type)
            {
                case ResourceType.Gold:
                    return _gold;
                case ResourceType.Gems:
                    return _gems;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }

        public TimeData GetLastSessionTimeData()
        {
            return _lastSessionTimeData;
        }

        private int CalculateTotalIdleMining()
        {
            var totalIdleMining = 0;
            for (int i = 0; i < _worldObjectLevels.Count; i++)
            {
                totalIdleMining += _worldObjectLevels[i].IdleMining;
            }

            return totalIdleMining;
        }
    }

    [Serializable]
    public class WorldObjectLevelData
    {
        public string Id;
        public int Level;
        public bool IsManagerAssigned;
        public int IdleMining;
    }

    [Serializable]
    public class TimeData
    {
        [SerializeField] private string _lastLaunchTime;
        [SerializeField] private bool _isCaptured;

        public bool IsCaptured()
        {
            return _isCaptured;
        }

        public void Capture()
        {
            _lastLaunchTime = DateTime.Now.ToBinary().ToString();
            _isCaptured = true;
        }

        public DateTime GetDateTime()
        {
            if (!_isCaptured || string.IsNullOrEmpty(_lastLaunchTime))
            {
                return DateTime.Now;
            }

            return DateTime.FromBinary(Convert.ToInt64(_lastLaunchTime));
        }
    }
}