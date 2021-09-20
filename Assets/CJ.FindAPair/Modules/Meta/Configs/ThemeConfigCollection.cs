using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CJ.FindAPair.Modules.Meta.Configs
{
    [CreateAssetMenu(fileName = "ThemeCollection", menuName = "Find a pair/ThemeCollection")]
    public class ThemeConfigCollection : ScriptableObject
    {
        [SerializeField] private string _defaultThemeId;
        [SerializeField] private List<ThemeConfig> _themes;

        public List<ThemeConfig> Themes => _themes;
        public string DefaultThemeId => _defaultThemeId;

        public ThemeConfig GetThemeConfig(string id)
        {
            return _themes.FirstOrDefault(theme => theme.Id == id);
        }
    }
}
