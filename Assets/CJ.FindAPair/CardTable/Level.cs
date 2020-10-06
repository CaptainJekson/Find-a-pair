using CJ.FindAPair.Configuration;
using System;
using UnityEngine;

namespace CJ.FindAPair.CardTable
{
    [Serializable]
    public class Level
    {
        public LevelConfig LevelConfig;
        [Range(0.0f, 1.0f)] public float ReductionRatio;
    }
}
