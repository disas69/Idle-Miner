using FSM;
using Game.Units;
using UnityEngine;

namespace Game.FSM.Actions
{
    [CreateAssetMenu(fileName = "IdleAction", menuName = "FSM/Actions/IdleAction")]
    public class IdleAction : FSMAction
    {
        private bool _isFinished;
        private Unit _unit;
        private Vector2 _idlePoint;

        public override bool IsFinished
        {
            get { return _isFinished; }
        }

        public override void Initialize(FSMController controller)
        {
            base.Initialize(controller);

            _unit = Controller.gameObject.GetComponent<Unit>();
            _idlePoint = _unit.IdlePoint;
        }

        public override void OnEnter()
        {
            _unit.transform.position = _idlePoint;
            _unit.Animator.SetTrigger("Idle");
        }

        public override void OnUpdate()
        {
            if (_unit.IsManagerAssigned)
            {
                _unit.TriggerManager();
                _isFinished = true;
            }
        }

        public override void OnExit()
        {
        }
    }
}