using System;

namespace Game.Core.Resources.Manager
{
    public interface IResourceManager
    {
        event Action<int> GemsAmountChanged;
        event Action<int> GoldAmountChanged;
        int GetResourceAmount(ResourceType resourceType);
        int GetTotalIdleMining();
        void Increase(ResourceType resourceType, int value);
        void Decrease(ResourceType resourceType, int value);
    }
}