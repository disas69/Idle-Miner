using Game.Controls;
using Game.Core.Resources;
using Game.Core.Resources.Manager;
using Game.Units;
using Game.Utils;
using UnityEngine;
using Zenject;

namespace Game.WorldObjects.Base
{
    public class WorldObjectViewOpened : MonoBehaviour
    {
        private int _upgradeCost;
        private int _managerCost;

        [Inject] private IResourceManager _resourceManager;
        [SerializeField] private GameButton _upgradeButton;
        [SerializeField] private GameButton _managerButton;
        [SerializeField] private Manager _managerView;
        [SerializeField] private TextMesh _managerCostText;

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);

            if (isActive)
            {
                var amount = _resourceManager.GetResourceAmount(ResourceType.Gold);
                _upgradeButton.SetActive(amount >= _upgradeCost);
                _managerButton.SetActive(amount >= _managerCost);

                _resourceManager.GoldAmountChanged += OnGoldAmountChanged;
            }
            else
            {
                _resourceManager.GoldAmountChanged -= OnGoldAmountChanged;
            }
        }

        public void Setup(bool isMaxed, int upgradeCost, int managerCost, bool isManagerAssigned)
        {
            _upgradeButton.gameObject.SetActive(!isMaxed);
            _upgradeCost = upgradeCost;
            _managerCost = managerCost;

            _managerCostText.text = FormatHelper.FormatCost(managerCost, ResourceType.Gold);
            _managerButton.gameObject.SetActive(!isManagerAssigned);
            _managerView.SetActive(isManagerAssigned);
        }

        private void OnGoldAmountChanged(int amount)
        {
            _upgradeButton.SetActive(amount >= _upgradeCost);
            _managerButton.SetActive(amount >= _managerCost);
        }
    }
}