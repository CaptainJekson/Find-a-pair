using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CJ.FindAPair.Cards.Config
{
    [CreateAssetMenu(fileName = "LevelConfigGroup", menuName = "Configs/LevelConfigGroup")]
    public class LevelConfigGroup : ScriptableObject
    {
        public RectTransform CardPrefab;
        public List<LevelConfig> LevelConfigs;
    }
}
