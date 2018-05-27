using System.Collections.Generic;
using Game.WorldObjects.Base.Configuration;
using UnityEngine;

namespace Game.WorldObjects.MineShaft.Configuration
{
    [CreateAssetMenu(fileName = "MineShaftConfiguration", menuName = "WorldObjects/Configs/MineShaftConfiguration")]
    public class MineShaftConfiguration : ScriptableObject, IWorldObjectConfiguration<IWorldObjectSettings>
    {
        [SerializeField] private string _id;
        [SerializeField] private int _managerCost;
        [SerializeField] private List<MineShaftSettings> _settingsList;

        public string Id
        {
            get { return _id; }
        }

        public int ManagerCost
        {
            get { return _managerCost; }
        }

        public List<MineShaftSettings> SettingsList
        {
            get { return _settingsList; }
        }

        IWorldObjectSettings IWorldObjectConfiguration<IWorldObjectSettings>.GetSettings(int level)
        {
            return GetSettings(level);
        }

        public MineShaftSettings GetSettings(int level)
        {
            var settings = SettingsList.Find(s => s.Level == level);
            if (settings != null)
            {
                return settings;
            }

            Debug.LogWarning(string.Format("Failed to find MineShaftSettings for level: {0}", level));
            return null;
        }
    }
}