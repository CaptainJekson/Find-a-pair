using System.Collections.Generic;
using UnityEngine;

namespace CJ.FindAPair.Cards.Configs
{
    [CreateAssetMenu(fileName = "LevelConfigGroup", menuName = "Configs/LevelConfigGroup")]
    public class LevelConfigGroup : ScriptableObject
    {
        public RectTransform CardPrefab;
        public List<LevelConfig> LevelConfigs;
    }
}
