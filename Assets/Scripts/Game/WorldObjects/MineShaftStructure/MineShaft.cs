using System.Collections;
using System.Collections.Generic;
using Game.Commands;
using Game.Units;
using Game.WorldObjects.Base;
using Game.WorldObjects.MineShaftStructure.Configuration;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.WorldObjects.MineShaftStructure
{
    public class MineShaft : WorldObject<MineShaftModel, MineShaftConfiguration>
    {
        private readonly List<Unit> _units = new List<Unit>();

        [Header("Settings")]
        [SerializeField] private Unit _minerPrefab;
        [SerializeField] private WorldObjectViewOpened _openedView;
        [SerializeField] private WorldObjectViewClosed _closedView;
        [SerializeField] private Manager _manager;
        [SerializeField] private Transform _unitsRoot;

        [Header("Points")]
        [SerializeField] private Transform _idlePoint;
        [SerializeField] private Transform _workPoint;

        [UsedImplicitly]
        public void Upgrade()
        {
            Model.Upgrade();
        }

        [UsedImplicitly]
        public void AssignManager()
        {
            if (Model.IsManagerAssigned)
            {
                return;
            }

            Model.AssignManager(true);
        }

        protected override void UpdateView()
        {
            base.UpdateView();

            if (Model.IsOpened)
            {
                _openedView.Setup(Model.IsMaxed, Model.UpgradeCost, Model.ManagerCost, Model.IsManagerAssigned);
                _openedView.SetActive(true);
                _closedView.SetActive(false);

                StartCoroutine(UpdateUnits());
            }
            else
            {
                _closedView.Setup(Model.UpgradeCost);
                _closedView.SetActive(true);
                _openedView.SetActive(false);
            }
        }

        private IEnumerator UpdateUnits()
        {
            for (int i = 0; i < _units.Count; i++)
            {
                _units[i].UpdateSettings(Model.Settings, _manager);
            }

            var wait = new WaitForSeconds(1f);
            while (_units.Count < Model.Settings.Units)
            {
                var unit = Instantiate(_minerPrefab, _unitsRoot);
                unit.Setup(new ActivateFSMStateCommand(unit, "GoToWork"), _idlePoint.position, _workPoint.position);
                unit.UpdateSettings(Model.Settings, _manager);
                unit.WorkFinished += OnWorkFinished;
                _units.Add(unit);

                yield return wait;
            }
        }

        private void OnWorkFinished(int amount)
        {
            Model.StoreGold(amount);
        }
    }
}