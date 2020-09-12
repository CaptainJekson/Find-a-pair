using System.Collections.Generic;
using UnityEngine;

namespace CJ.FindAPair.Themes.Configs
{
    [CreateAssetMenu(fileName = "ThemeConfig", menuName = "Configs/ThemeConfig")]
    public class ThemeConfig : ScriptableObject
    {
        public Sprite Shirt;
        public List<Sprite> PairsImage;
    }
}

