using System.Collections.Generic;
using UnityEngine;

namespace CJ.FindAPair.Themes.Configs
{
    [CreateAssetMenu(fileName = "ThemeConfig", menuName = "Configs/ThemeConfigGroup")]
    class ThemeConfigGroup : ScriptableObject
    {
        public List<ThemeConfig> ThemeConfigs;
    }
}
