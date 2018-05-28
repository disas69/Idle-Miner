using System;
using Extensions;
using Game.Core.Session;
using UnityEngine;
using Zenject;

namespace Game.Core.Resources.Manager
{
    public class ResourceManager : IResourceManager, IDisposable
    {
        private IGameSession _gameSession;

        public event Action<int> GemsAmountChanged;
        public event Action<int> GoldAmountChanged;

        [Inject]
        public ResourceManager(IGameSession gameSession)
        {
            _gameSession = gameSession;

            var gems = _gameSession.Data.GetResource(ResourceType.Gems);
            var gold = _gameSession.Data.GetResource(ResourceType.Gold);

            gems.AmountChanged += OnGemsAmountChanged;
            gold.AmountChanged += OnGoldAmountChanged;
        }

        public int GetResourceAmount(ResourceType resourceType)
        {
            return _gameSession.Data.GetResource(resourceType).Amount;
        }

        public int GetTotalIdleMining()
        {
            return _gameSession.Data.TotalIdleMining;
        }

        public void Increase(ResourceType resourceType, int value)
        {
            _gameSession.Data.GetResource(resourceType).Increase(value);

            UnityEngine.Debug.Log(string.Format("{0} earned: {1}. Total {0}: {2}", resourceType, value,
                _gameSession.Data.GetResource(resourceType).Amount));
        }

        public void Decrease(ResourceType resourceType, int value)
        {
            _gameSession.Data.GetResource(resourceType).Decrease(value);

            UnityEngine.Debug.Log(string.Format("{0} spent: {1}. Total {0}: {2}", resourceType, value,
                _gameSession.Data.GetResource(resourceType).Amount));
        }

        private void OnGemsAmountChanged(int amount)
        {
            GemsAmountChanged.SafeInvoke(amount);
        }

        private void OnGoldAmountChanged(int amount)
        {
            GoldAmountChanged.SafeInvoke(amount);
        }

        public void Dispose()
        {
            var gems = _gameSession.Data.GetResource(ResourceType.Gems);
            var gold = _gameSession.Data.GetResource(ResourceType.Gold);

            gems.AmountChanged -= OnGemsAmountChanged;
            gold.AmountChanged -= OnGoldAmountChanged;
        }
    }
}