using System.Collections.Generic;
using CJ.FindAPair.Modules.CutScenes.Configs.Base;
using UnityEngine;

namespace CJ.FindAPair.Modules.CutScenes.Installer
{
    [CreateAssetMenu(fileName = "CutScenesConfigs", menuName = "Find a pair/CutScenesConfigsCollection")]
    public class CutScenesConfigs : ScriptableObject
    {
        [SerializeField] private List<CutSceneConfig> _configs;

        public T GetConfig<T>() where T : CutSceneConfig
        {
            foreach (var config in _configs)
            {
                if (config.GetType() == typeof(T))
                    return config as T;
            }
        
            return null;
        }
    }
}