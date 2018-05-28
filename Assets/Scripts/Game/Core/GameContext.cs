using System.Collections;
using Game.Core.Data;
using Game.Core.Resources.Manager;
using Game.Core.Session;
using Game.UI.Popups;
using Game.UI.Screens;
using Game.WorldObjects.Base;
using Tools.Gameplay;
using UI.Structure;
using UnityEngine;
using Zenject;

namespace Game.Core
{
    public class GameContext : MonoBehaviour
    {
        private StateMachine<GameState> _gameStateMachine;

        [SerializeField] private Transform _worldRoot;

        public static IGameSession Session { get; private set; }
        public static IResourceManager ResourceManager { get; private set; }
        public static INavigationProvider NavigationProvider { get; private set; }

        [Inject]
        public void Construct(IGameSession gameSession, IResourceManager resourceManager,
            INavigationProvider navigationProvider)
        {
            Session = gameSession;
            ResourceManager = resourceManager;
            NavigationProvider = navigationProvider;

            _gameStateMachine = new StateMachine<GameState>(GameState.Initialization);
            _gameStateMachine.AddTransition(GameState.Initialization, GameState.Play, StartGame);

            NavigationProvider.OpenScreen<LoadingPage>();
            StartCoroutine(LoadGame(Session.Data));
        }

        private IEnumerator LoadGame(IGameData gameData)
        {
            //Waiter is used just to simulate a complex task processing
            var waiter = new WaitForSeconds(0.5f);

            var worldObjects = _worldRoot.GetComponentsInChildren<IWorldObject>(true);
            for (int i = 0; i < worldObjects.Length; i++)
            {
                worldObjects[i].Initialize(gameData);
                yield return waiter;
            }

            _gameStateMachine.SetState(GameState.Play);
        }

        private void StartGame()
        {
            NavigationProvider.OpenScreen<PlayPage>();

            if (Session.IdleMiningResul > 0)
            {
                NavigationProvider.ShowPopup<IdleMiningResultPopup>();
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                SaveSession();
            }
        }

        private void OnApplicationQuit()
        {
            SaveSession();
        }

        private void OnDestroy()
        {
            SaveSession();
        }

        private static void SaveSession()
        {
            Session.Data.GetLastSessionTimeData().Capture();
            Session.Save();
        }
    }
}