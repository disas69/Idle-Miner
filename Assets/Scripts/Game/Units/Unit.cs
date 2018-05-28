using System;
using Framework.Extensions;
using Framework.Tools.FSM;
using Game.Commands;
using Game.Core.Resources;
using Game.Core.Resources.Currency;
using Game.WorldObjects.Base.Configuration;
using UnityEngine;

namespace Game.Units
{
    [RequireComponent(typeof(CommandHandler), typeof(FSMController), typeof(Animator))]
    public class Unit : MonoBehaviour
    {
        private CommandHandler _commandHandler;
        private FSMController _fsmController;
        private Manager _manager;
        private IResource _gold;

        public event Action<int> WorkFinished;

        public Animator Animator { get; private set; }
        public Vector2 IdlePoint { get; private set; }
        public Vector2 ExtractPoint { get; private set; }
        public int Load { get; private set; }
        public float MoveTime { get; private set; }
        public float WorkTime { get; private set; }
        public bool IsManagerAssigned { get; private set; }

        private void Awake()
        {
            _commandHandler = GetComponent<CommandHandler>();
            _fsmController = GetComponent<FSMController>();
            _gold = new GoldResource();

            Animator = GetComponent<Animator>();
        }

        public void Setup(ICommand command, Vector2 idlePoint, Vector2 extractPoint)
        {
            _commandHandler.SetCommand(command);

            IdlePoint = idlePoint;
            ExtractPoint = extractPoint;
            transform.position = idlePoint;
        }

        public void ActivateState(string stateName)
        {
            if (_fsmController.CurrentState.Name == "Idle")
            {
                _fsmController.TransitionToState(stateName);
            }
        }

        public void UpdateSettings(IWorldObjectSettings settings, Manager manager)
        {
            Load = settings.Load;
            MoveTime = settings.MoveTime;
            WorkTime = settings.WorkTime;
            IsManagerAssigned = manager.IsAssigned;
            _manager = manager;
        }

        public void ExtractGold()
        {
            _gold.Increase(Load);
        }

        public void FinishWork()
        {
            WorkFinished.SafeInvoke(_gold.Amount);
            _gold = new GoldResource();
        }

        public void TriggerManager()
        {
            _manager.Triggered();
        }
    }
}