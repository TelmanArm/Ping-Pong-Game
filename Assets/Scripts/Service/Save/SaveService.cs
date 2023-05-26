using System.IO;
using Game;
using UnityEngine;

namespace Service.Save
{
    public class SaveService
    {
        private readonly string _path;

        public SaveService()
        {
            _path = Application.persistentDataPath + "/saveData.json";
        }

        public void Save(GameData gameData)
        {
            Debug.Log("Start save Game data");
            string jsonString = JsonUtility.ToJson(gameData);
            File.WriteAllText(_path, jsonString);
            Debug.Log("Game data was saved");
        }

        public GameData LoadData()
        {
            GameData gameData = null;
            if (File.Exists(_path))
            {
                Debug.Log("Data exist");
                string jsonString =  File.ReadAllText(_path);
                gameData = JsonUtility.FromJson<GameData>(jsonString);
            }
            else
            {
                Debug.Log("Data is null");
            }

            return gameData;
        }
        
        public void DeleteFile()
        {
            if(File.Exists(_path))
            {
                File.Delete(_path);
            }
            else
            {
                Debug.Log("No file exists at " + _path);
            }
        }
    }
}