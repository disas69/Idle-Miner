﻿using UnityEngine;

namespace FSM
{
    public abstract class FSMCondition : ScriptableObject
    {
        protected FSMController Controller;

        public virtual void Initialize(FSMController controller)
        {
            Controller = controller;
        }

        public abstract bool Check();
    }
}