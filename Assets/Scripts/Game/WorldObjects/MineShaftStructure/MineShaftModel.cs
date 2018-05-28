using Game.Core;
using Game.Core.Resources;
using Game.WorldObjects.Base;
using Game.WorldObjects.MineShaftStructure.Configuration;

namespace Game.WorldObjects.MineShaftStructure
{
    public class MineShaftModel : WorldObjectModel<MineShaftConfiguration>
    {
        //TODO: Put the gold in temporary storage so the Elevator can take it
        public void StoreGold(int amount)
        {
            GameContext.ResourceManager.Increase(ResourceType.Gold, amount);
        }
    }
}