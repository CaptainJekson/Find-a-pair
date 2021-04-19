using CJ.FindAPair.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CJ.FindAPair.Configurations
{
    [CreateAssetMenu(fileName = "LevelCollection", menuName = "Find a pair/LevelCollection")]
    class LevelConfigCollection : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> _levels;

        public List<LevelConfig> Levels => _levels;
    }
}
