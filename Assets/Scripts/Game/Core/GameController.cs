using Game.Core.Data;
using Game.Core.Session;
using Game.WorldObjects.Base;
using UnityEngine;
using Zenject;

namespace Game.Core
{
    public class GameController : MonoBehaviour
    {
        [Inject] private IGameSession _gameSession;
        [SerializeField] private Transform _worldRoot;

        private void Awake()
        {
            if (_gameSession.IsInitialized)
            {
                Initialize(_gameSession.Data);
            }
            else
            {
                _gameSession.Initialized += Initialize;
            }
        }

        private void Initialize(IGameData gameData)
        {
            var worldObjects = _worldRoot.GetComponentsInChildren<IWorldObject>(true);
            for (int i = 0; i < worldObjects.Length; i++)
            {
                worldObjects[i].Initialize(gameData);
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                _gameSession.Data.GetLastSessionTimeData().Capture();
                _gameSession.Save();
            }
        }

        private void OnApplicationQuit()
        {
            _gameSession.Data.GetLastSessionTimeData().Capture();
            _gameSession.Save();
        }

        private void OnDestroy()
        {
            _gameSession.Initialized -= Initialize;
            _gameSession.Data.GetLastSessionTimeData().Capture();
            _gameSession.Save();
        }
    }
}