using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CJ.FindAPair.Cards.Config
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public int Tries;
        public float Time;
        public int QuantityInPair; 

        public float ScaleCard;
        public List<Vector2Int> PositionCard;
    }
}

