using System.Collections.Generic;
using Game.WorldObjects.Base.Configuration;
using UnityEngine;

namespace Game.WorldObjects.WarehouseStructure.Configuration
{
    [CreateAssetMenu(fileName = "WarehouseConfiguration", menuName = "WorldObjects/Configs/WarehouseConfiguration")]
    public class WarehouseConfiguration : ScriptableObject, IWorldObjectConfiguration<IWorldObjectSettings>
    {
        [SerializeField] private string _id;
        [SerializeField] private int _managerCost;
        [SerializeField] private List<WarehouseSettings> _settingsList;

        public string Id
        {
            get { return _id; }
        }

        public int ManagerCost
        {
            get { return _managerCost; }
        }

        public List<WarehouseSettings> SettingsList
        {
            get { return _settingsList; }
        }

        IWorldObjectSettings IWorldObjectConfiguration<IWorldObjectSettings>.GetSettings(int level)
        {
            return GetSettings(level);
        }

        private WarehouseSettings GetSettings(int level)
        {
            var settings = SettingsList.Find(s => s.Level == level);
            if (settings != null)
            {
                return settings;
            }

            UnityEngine.Debug.LogWarning(string.Format("Failed to find WarehouseSettings for level: {0}", level));
            return null;
        }
    }
}