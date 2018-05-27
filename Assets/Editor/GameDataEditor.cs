using Game.Core.Data;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class GameDataEditor
    {
        [MenuItem("Idle Miner/Delete GameData")]
        public static void ClearData()
        {
            FileUtil.DeleteFileOrDirectory(Application.persistentDataPath + "/" + DataKeeper.FileName);
        }
    }
}