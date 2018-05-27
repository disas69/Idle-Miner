using Framework.UI.Structure.Base.View;
using Game.Core.Resources;
using Game.Core.Resources.Manager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Screens
{
    public class PlayPage : Page<PlayPageModel>
    {
        private const string IdleMiningTemplate = "{0}/s";

        private int _idleManingValueStored;

        [Inject] private IResourceManager _resourceManager;
        [SerializeField] private Text _idleMiningText;
        [SerializeField] private Text _gemsAmountText;
        [SerializeField] private Text _goldAmountText;

        protected override void Awake()
        {
            base.Awake();

            _idleManingValueStored = _resourceManager.GetTotalIdleMining();
            _idleMiningText.text = string.Format(IdleMiningTemplate, _idleManingValueStored);
            _gemsAmountText.text = _resourceManager.GetResourceAmount(ResourceType.Gems).ToString();
            _goldAmountText.text = _resourceManager.GetResourceAmount(ResourceType.Gold).ToString();

            _resourceManager.GemsAmountChanged += OnGemsAmountChanged;
            _resourceManager.GoldAmountChanged += OnGoldAmountChanged;
        }

        public override void Update()
        {
            base.Update();

            var idleManingValue = _resourceManager.GetTotalIdleMining();
            if (_idleManingValueStored != idleManingValue)
            {
                _idleManingValueStored = idleManingValue;
                _idleMiningText.text = string.Format(IdleMiningTemplate, _idleManingValueStored);
            }
        }

        private void OnGemsAmountChanged(int amount)
        {
            _gemsAmountText.text = _resourceManager.GetResourceAmount(ResourceType.Gems).ToString();
        }

        private void OnGoldAmountChanged(int amount)
        {
            _goldAmountText.text = _resourceManager.GetResourceAmount(ResourceType.Gold).ToString();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            _resourceManager.GemsAmountChanged += OnGemsAmountChanged;
            _resourceManager.GoldAmountChanged += OnGoldAmountChanged;
        }
    }
}