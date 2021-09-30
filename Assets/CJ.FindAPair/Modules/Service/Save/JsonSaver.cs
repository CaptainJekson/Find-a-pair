using System.IO;
using UnityEngine;

namespace CJ.FindAPair.Modules.Service.Save
{
    public class JsonSaver : ISaver
    {
        private static SaveData _saveData;
        private static string _path;

        public JsonSaver()
        {
            _saveData = new SaveData();

#if UNITY_ANDROID && !UNITY_EDITOR
            _path = Path.Combine(Application.persistentDataPath, "SaveData.json");
#else
            _path = Path.Combine(Application.dataPath, "SaveData.json");
#endif
            if (File.Exists(_path))
            {
                _saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(_path));
            }
            else
            {
                File.WriteAllText(_path, JsonUtility.ToJson(_saveData));
            }
        }

        public SaveData LoadData()
        {
            return _saveData;
        }

        public void SaveData(SaveData saveData)
        {
            File.WriteAllText(_path, JsonUtility.ToJson(saveData));
        }
    }
}