using Game.Controls;
using Game.Core.Resources;
using Game.Core.Resources.Manager;
using Game.Utils;
using UnityEngine;
using Zenject;

namespace Game.WorldObjects.Base
{
    public class WorldObjectViewClosed : MonoBehaviour
    {
        private int _upgradeCost;

        [Inject] private IResourceManager _resourceManager;
        [SerializeField] private GameButton _activateButton;
        [SerializeField] private TextMesh _upgradeCostText;

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);

            if (isActive)
            {
                var amount = _resourceManager.GetResourceAmount(ResourceType.Gold);
                _activateButton.SetActive(amount >= _upgradeCost);
                _resourceManager.GoldAmountChanged += OnGoldAmountChanged;
            }
            else
            {
                _resourceManager.GoldAmountChanged -= OnGoldAmountChanged;
            }
        }

        public void Setup(int upgradeCost)
        {
            _upgradeCost = upgradeCost;
            _upgradeCostText.text = FormatHelper.FormatCost(upgradeCost, ResourceType.Gold);
        }

        private void OnGoldAmountChanged(int amount)
        {
            _activateButton.SetActive(amount >= _upgradeCost);
        }
    }
}