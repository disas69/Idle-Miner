using Framework.Tools.FSM;
using UnityEngine;

namespace Game.FSM.Conditions
{
    [CreateAssetMenu(fileName = "IsActionFinished", menuName = "FSM/Conditions/IsActionFinished")]
    public class IsActionFinished : FSMCondition
    {
        public override bool Check()
        {
            return Controller.CurrentState.Action.IsFinished;
        }
    }
}