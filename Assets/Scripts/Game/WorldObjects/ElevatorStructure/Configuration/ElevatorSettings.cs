﻿using System;
using Game.WorldObjects.Base.Configuration;
using UnityEngine;

namespace Game.WorldObjects.ElevatorStructure.Configuration
{
    [Serializable]
    public class ElevatorSettings : IWorldObjectSettings
    {
        [SerializeField] private int _level;
        [SerializeField] private int _upgradeCost;
        [SerializeField] private int _units = 1; //Field is not editable in the Editor since the Elevator has only one unit
        [SerializeField] private int _load;
        [SerializeField] private float _moveTime;
        [SerializeField] private float _workTime;

        public int Level
        {
            get { return _level; }
        }

        public int UpgradeCost
        {
            get { return _upgradeCost; }
        }

        public int Units
        {
            get { return _units; }
        }

        public int Load
        {
            get { return _load; }
        }

        public float MoveTime
        {
            get { return _moveTime; }
        }

        public float WorkTime
        {
            get { return _workTime; }
        }
    }
}