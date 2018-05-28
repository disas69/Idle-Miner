using Game.Core;
using Game.Core.Resources;
using Game.Utils;
using UI.Structure.Base.Model;
using UI.Structure.Base.View;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Screens
{
    public class PlayPage : Page<PageModel>
    {
        private int _idleManingValueStored;

        [SerializeField] private Text _idleMiningText;
        [SerializeField] private Text _gemsAmountText;
        [SerializeField] private Text _goldAmountText;

        protected override void Awake()
        {
            base.Awake();

            _idleManingValueStored = GameContext.ResourceManager.GetTotalIdleMining();
            _idleMiningText.text = FormatHelper.FormatIdleMining(_idleManingValueStored);
            _gemsAmountText.text =
                FormatHelper.FormatResourceAmount(GameContext.ResourceManager.GetResourceAmount(ResourceType.Gems));
            _goldAmountText.text =
                FormatHelper.FormatResourceAmount(GameContext.ResourceManager.GetResourceAmount(ResourceType.Gold));

            GameContext.ResourceManager.GemsAmountChanged += OnGemsAmountChanged;
            GameContext.ResourceManager.GoldAmountChanged += OnGoldAmountChanged;
        }

        public override void Update()
        {
            base.Update();

            var idleManingValue = GameContext.ResourceManager.GetTotalIdleMining();
            if (_idleManingValueStored != idleManingValue)
            {
                _idleManingValueStored = idleManingValue;
                _idleMiningText.text = FormatHelper.FormatIdleMining(_idleManingValueStored);
            }
        }

        private void OnGemsAmountChanged(int amount)
        {
            _gemsAmountText.text =
                FormatHelper.FormatResourceAmount(GameContext.ResourceManager.GetResourceAmount(ResourceType.Gems));
        }

        private void OnGoldAmountChanged(int amount)
        {
            _goldAmountText.text =
                FormatHelper.FormatResourceAmount(GameContext.ResourceManager.GetResourceAmount(ResourceType.Gold));
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            GameContext.ResourceManager.GemsAmountChanged += OnGemsAmountChanged;
            GameContext.ResourceManager.GoldAmountChanged += OnGoldAmountChanged;
        }
    }
}