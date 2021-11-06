using System;
using System.IO;
using CJ.FindAPair.Modules.Service.Server;
using UnityEngine;

namespace CJ.FindAPair.Modules.Service.Save
{
    public class JsonSaver : ISaver
    {
        private SaveData _saveData;
        private string _jsonSaveData;
        private string _path;

        private ServerConnector _serverConnector;
        
        public JsonSaver(ServerConnector serverConnector)
        {
            _serverConnector = serverConnector;
            _saveData = new SaveData();

#if UNITY_ANDROID && !UNITY_EDITOR
            _path = Path.Combine(Application.persistentDataPath, "SaveData.json");
#else
            _path = Path.Combine(Application.dataPath, "SaveData.json");
#endif
            if (File.Exists(_path)) 
            {
                _jsonSaveData = File.ReadAllText(_path);
                _saveData = JsonUtility.FromJson<SaveData>(_jsonSaveData);
                _serverConnector.LoadSave(_saveData.UserId, OnCompleteLoadSave);
            }
            else
            {
                _jsonSaveData = JsonUtility.ToJson(_saveData);
                _serverConnector.CreateSave(_jsonSaveData, OnCompleteCreateSave);
            }
        }

        public SaveData LoadData()
        {
            return _saveData;
        }

        public void SaveData(SaveData saveData)
        {
            if(saveData.UserId == 0)
                return;
            
            _jsonSaveData = JsonUtility.ToJson(saveData);
            File.WriteAllText(_path, _jsonSaveData);
            _serverConnector.UpdateSave(_jsonSaveData);
        }

        private void OnCompleteCreateSave(string userId)
        {
            _saveData.UserId = Convert.ToInt32(userId);
            _jsonSaveData = JsonUtility.ToJson(_saveData);
            File.WriteAllText(_path, _jsonSaveData);
            SaveData(_saveData);
        }
        
        private void OnCompleteLoadSave(string jsonSaveData)
        {
            _saveData = JsonUtility.FromJson<SaveData>(jsonSaveData);
        }
    }
}