using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace CJ.FindAPair.Game
{
    public class GameSaver : MonoBehaviour
    {
        private Save _save;
        private string _path;

        public event UnityAction OnSaved;

        private void Awake()
        {
            _save = new Save();

#if UNITY_ANDROID && !UNITY_EDITOR
            _path = Path.Combine(Application.persistentDataPath, "Save.json");
#else
            _path = Path.Combine(Application.dataPath, "Save.json");
#endif
            if (File.Exists(_path))
            {
                _save = JsonUtility.FromJson<Save>(File.ReadAllText(_path));
            }
            
            OnSaved?.Invoke();
        }

        public void SaveInt(SaveTypeInt saveTypeInt, int value)
        {
            switch (saveTypeInt)
            {
                case SaveTypeInt.Score:
                    _save.Score += value;
                    break;
                case SaveTypeInt.Energy:
                    _save.Energy += value;
                    break;
                case SaveTypeInt.MagicEye:
                    _save.MagicEye += value;
                    break;
                case SaveTypeInt.Electroshock:
                    _save.Electroshock += value;
                    break;
                case SaveTypeInt.Sapper:
                    _save.Sapper += value;
                    break;
                default:
                    throw new Exception("save type not selected");
            }

            File.WriteAllText(_path, JsonUtility.ToJson(_save));
            
            OnSaved?.Invoke();
        }
        
        public int LoadInt(SaveTypeInt saveTypeInt)
        {
            switch (saveTypeInt)
            {
                case SaveTypeInt.Score:
                    return _save.Score;
                case SaveTypeInt.Energy:
                    return _save.Energy;
                case SaveTypeInt.MagicEye:
                    return _save.MagicEye;
                case SaveTypeInt.Electroshock:
                    return _save.Electroshock;
                case SaveTypeInt.Sapper:
                    return _save.Sapper;
                default:
                    throw new Exception("load type not selected");
            }

            return 0;
        }

        public void SaveString(SaveTypeString saveTypeString)
        {
        }

        public void SaveDateTime(SaveTypeDateTime saveTypeDateTime)
        {
        }
    }
}