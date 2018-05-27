using System.Collections;
using System.Collections.Generic;
using Game.Commands;
using Game.Units;
using Game.WorldObjects.Base;
using Game.WorldObjects.MineShaft.Configuration;
using UnityEngine;

namespace Game.WorldObjects.MineShaft
{
    public class MineShaftView : WorldObject<MineShaftModel, MineShaftConfiguration>
    {
        private readonly List<Unit> _units = new List<Unit>();

        [SerializeField] private Unit _unitPrefab;
        [SerializeField] private MineShaftViewOpened _openedState;
        [SerializeField] private MineShaftViewClosed _closedState;
        [SerializeField] private Transform _unitsRoot;
        [SerializeField] private Transform _idlePoint;
        [SerializeField] private Transform _extractPoint;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void OnModelStateChanged()
        {
            base.OnModelStateChanged();

            if (Model.IsOpened)
            {
                _openedState.Setup(Model.IsMaxed, Model.UpgradeCost, Model.ManagerCost, Model.IsManagerAssigned);
                _openedState.SetActive(true);
                _closedState.SetActive(false);

                StartCoroutine(UpdateUnits());
            }
            else
            {
                _closedState.Setup(Model.UpgradeCost);
                _closedState.SetActive(true);
                _openedState.SetActive(false);
            }
        }

        private IEnumerator UpdateUnits()
        {
            for (int i = 0; i < _units.Count; i++)
            {
                _units[i].UpdateSettings(Model.Settings, Model.IsManagerAssigned);
            }

            var wait = new WaitForSeconds(1f);
            while (_units.Count < Model.Settings.Units)
            {
                var unit = Instantiate(_unitPrefab, _unitsRoot);
                unit.Setup(new ActivateFSMStateCommand(unit, "GoToWork"), _idlePoint.position, _extractPoint.position);
                unit.GoldEarned += OnGoldEarned;
                unit.UpdateSettings(Model.Settings, Model.IsManagerAssigned);
                _units.Add(unit);

                yield return wait;
            }
        }

        private void OnGoldEarned(int amount)
        {
            Model.StoreGold(amount);
        }

        protected override void Update()
        {
            base.Update();
        }

        public void Upgrade()
        {
            Model.Upgrade();
        }

        public void AssignManager()
        {
            if (Model.IsManagerAssigned)
            {
                return;
            }

            Model.AssignManager(true);
        }
    }
}