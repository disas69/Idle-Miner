using Game.Units;

namespace Game.Commands
{
    public class ActivateFSMStateCommand : ICommand
    {
        private readonly Unit _unit;
        private readonly string _stateName;

        public ActivateFSMStateCommand(Unit unit, string stateName)
        {
            _unit = unit;
            _stateName = stateName;
        }

        public void Execute()
        {
            _unit.ActivateState(_stateName);
        }
    }
}