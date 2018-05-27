using System;
using Framework.Extensions;
using UnityEngine;

namespace Game.Core.Resources.Currency
{
    [Serializable]
    public class GoldResource : IResource
    {
        [SerializeField] private int _amount;

        public event Action<int> AmountChanged;

        public ResourceType Type
        {
            get { return ResourceType.Gold; }
        }

        public int Amount
        {
            get { return _amount; }
        }

        public GoldResource(int initialAmount = 0)
        {
            _amount = initialAmount;
        }

        public void Increase(int value)
        {
            _amount += value;
            AmountChanged.SafeInvoke(_amount);
        }

        public void Decrease(int value)
        {
            _amount -= value;
            if (_amount < 0)
            {
                _amount = 0;
            }

            AmountChanged.SafeInvoke(_amount);
        }
    }
}