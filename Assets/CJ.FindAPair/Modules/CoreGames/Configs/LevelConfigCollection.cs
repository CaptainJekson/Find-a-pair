using System.Collections.Generic;
using UnityEngine;

namespace CJ.FindAPair.Modules.CoreGames.Configs
{
    [CreateAssetMenu(fileName = "LevelCollection", menuName = "Find a pair/LevelCollection")]
    public class LevelConfigCollection : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> _levels;

        public List<LevelConfig> Levels => _levels;
    }
}
