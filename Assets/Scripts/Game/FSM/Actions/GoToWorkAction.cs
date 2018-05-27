using Framework.Tools.FSM;
using Game.Units;
using UnityEngine;

namespace Game.FSM.Actions
{
    [CreateAssetMenu(fileName = "GoToWorkAction", menuName = "FSM/Actions/GoToWorkAction")]
    public class GoToWorkAction : FSMAction
    {
        private bool _isFinished;
        private Unit _unit;
        private Vector2 _idlePoint;
        private Vector2 _workPoint;
        private float _time;

        public override bool IsFinished
        {
            get { return _isFinished; }
        }

        public override void Initialize(FSMController controller)
        {
            base.Initialize(controller);

            _unit = Controller.gameObject.GetComponent<Unit>();
            _idlePoint = _unit.IdlePoint;
            _workPoint = _unit.ExtractPoint;
        }

        public override void OnEnter()
        {
            _isFinished = false;
            _time = 0f;
        }

        public override void OnUpdate()
        {
            _unit.transform.position = Vector2.Lerp(_idlePoint, _workPoint, _time / _unit.MoveTime);
            _time += Time.deltaTime;

            if (_time >= _unit.MoveTime)
            {
                _isFinished = true;
            }
        }

        public override void OnExit()
        {
        }
    }
}