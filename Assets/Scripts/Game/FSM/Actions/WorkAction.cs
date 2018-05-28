using Framework.Tools.FSM;
using Game.Units;
using UnityEngine;

namespace Game.FSM.Actions
{
    [CreateAssetMenu(fileName = "WorkAction", menuName = "FSM/Actions/WorkAction")]
    public class WorkAction : FSMAction
    {
        private bool _isFinished;
        private Unit _unit;
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
            _workPoint = _unit.ExtractPoint;
        }

        public override void OnEnter()
        {
            _unit.transform.position = _workPoint;
            _unit.Animator.SetTrigger("Work");
            _isFinished = false;
            _time = 0f;
        }

        public override void OnUpdate()
        {
            _time += Time.deltaTime;

            if (_time >= _unit.WorkTime)
            {
                _unit.ExtractGold();
                _isFinished = true;
            }
        }

        public override void OnExit()
        {
        }
    }
}