using Game.Core.Data;
using Game.Core.Resources;
using Game.WorldObjects.Base;
using Game.WorldObjects.MineShaft.Configuration;

namespace Game.WorldObjects.MineShaft
{
    public class MineShaftModel : WorldObjectModel<MineShaftConfiguration>
    {
        public override void Initialize(IGameData gameData, MineShaftConfiguration configuration)
        {
            base.Initialize(gameData, configuration);
        }

        public override void Update()
        {
            base.Update();
        }

        public void StoreGold(int amount)
        {
            GameData.GetResource(ResourceType.Gold).Increase(amount);
        }
    }
}