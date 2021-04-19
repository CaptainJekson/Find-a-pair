using System;
using System.IO;
using CJ.FindAPair.Game.Booster;
using UnityEngine;
using UnityEngine.Events;

namespace CJ.FindAPair.Game
{
    public static class GameSaver
    {
        private static Save _save;
        private static string _path;

        public static event UnityAction OnSaved;

        public static void Init()
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

        public static void SaveResources(PlayerResourcesType playerResourcesType, int value)
        {
            switch (playerResourcesType)
            {
                case PlayerResourcesType.Gold:
                    _save.Gold += value;
                    break;
                case PlayerResourcesType.Energy:
                    _save.Energy += value;
                    break;
                default:
                    throw new Exception("save type not selected");
            }

            File.WriteAllText(_path, JsonUtility.ToJson(_save));
            
            OnSaved?.Invoke();
        }
        
        public static int LoadResources(PlayerResourcesType playerResourcesType)
        {
            switch (playerResourcesType)
            {
                case PlayerResourcesType.Gold:
                    return _save.Gold;
                case PlayerResourcesType.Energy:
                    return _save.Energy;
                default:
                    throw new Exception("load type not selected");
            }
        }
        
        public static void SaveBooster(BoosterType boosterType, int value)
        {
            switch (boosterType)
            {
                case BoosterType.MagicEye:
                    _save.MagicEye += value;
                    break;
                case BoosterType.Electroshock:
                    _save.Electroshock += value;
                    break;
                case BoosterType.Sapper:
                    _save.Sapper += value;
                    break;
                default:
                    throw new Exception("load type not selected");
            }
            
            File.WriteAllText(_path, JsonUtility.ToJson(_save));
            
            OnSaved?.Invoke();
        }

        public static int LoadBooster(BoosterType boosterType)
        {
            switch (boosterType)
            {
                case BoosterType.MagicEye:
                    return _save.MagicEye;
                case BoosterType.Electroshock:
                    return _save.Electroshock;
                case BoosterType.Sapper:
                    return _save.Sapper;
                default:
                    throw new Exception("load type not selected");
            }
        }

        public static void SaveString(SaveTypeString saveTypeString)
        {
        }

        public static void SaveDateTime(SaveTypeDateTime saveTypeDateTime)
        {
        }
    }
}