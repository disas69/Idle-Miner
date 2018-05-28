using Game.Core.Data;
using Game.WorldObjects.Base.Configuration;
using UnityEngine;

namespace Game.WorldObjects.Base
{
    public abstract class WorldObject<T1, T2> : MonoBehaviour, IWorldObject where T1 : WorldObjectModel<T2>, new()
        where T2 : IWorldObjectConfiguration<IWorldObjectSettings>
    {
        private T1 _model;
        private bool _isInitialized;

        [SerializeField] private T2 _configuration;

        public T1 Model
        {
            get { return _model; }
        }

        protected virtual void Awake()
        {
        }

        public void Initialize(IGameData gameData)
        {
            _model = new T1();
            _model.StateChanged += OnModelStateChanged;
            _model.Initialize(gameData, _configuration);
            _isInitialized = true;
        }

        private void OnModelStateChanged()
        {
            UpdateView();
        }

        protected virtual void UpdateView()
        {
        }

        protected virtual void Update()
        {
            if (_isInitialized)
            {
                _model.Update();
            }
        }

        protected virtual void OnDestroy()
        {
            _model.StateChanged -= OnModelStateChanged;
        }
    }
}