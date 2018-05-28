using Data;
using UnityEngine;

namespace Game.Core.Data
{
    public class DataKeeper
    {
        private readonly JsonDataKeeper<GameData> _dataKeeper;

        public const string FileName = "GameData";
        public GameData Data { get; private set; }

        public DataKeeper()
        {
            _dataKeeper = new JsonDataKeeper<GameData>(Application.persistentDataPath + "/" + FileName, true);
        }

        public void Load()
        {
            Data = _dataKeeper.Load();
        }

        public void Save()
        {
            _dataKeeper.Save(Data);
        }
    }
}