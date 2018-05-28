using System;

namespace Game.Core.Resources
{
    public interface IResource
    {
        event Action<int> AmountChanged;
        ResourceType Type { get; }
        int Amount { get; }
        void Increase(int value);
        void Decrease(int value);
        void Reset();
    }
}