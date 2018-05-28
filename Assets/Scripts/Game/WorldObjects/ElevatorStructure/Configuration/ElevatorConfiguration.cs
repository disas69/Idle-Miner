using System.Collections.Generic;
using Game.WorldObjects.Base.Configuration;
using UnityEngine;

namespace Game.WorldObjects.ElevatorStructure.Configuration
{
    [CreateAssetMenu(fileName = "ElevatorConfiguration", menuName = "WorldObjects/Configs/ElevatorConfiguration")]
    public class ElevatorConfiguration : ScriptableObject, IWorldObjectConfiguration<IWorldObjectSettings>
    {
        [SerializeField] private string _id;
        [SerializeField] private int _managerCost;
        [SerializeField] private List<ElevatorSettings> _settingsList;

        public string Id
        {
            get { return _id; }
        }

        public int ManagerCost
        {
            get { return _managerCost; }
        }

        public List<ElevatorSettings> SettingsList
        {
            get { return _settingsList; }
        }

        IWorldObjectSettings IWorldObjectConfiguration<IWorldObjectSettings>.GetSettings(int level)
        {
            return GetSettings(level);
        }

        private ElevatorSettings GetSettings(int level)
        {
            var settings = SettingsList.Find(s => s.Level == level);
            if (settings != null)
            {
                return settings;
            }

            UnityEngine.Debug.LogWarning(string.Format("Failed to find ElevatorSettings for level: {0}", level));
            return null;
        }
    }
}