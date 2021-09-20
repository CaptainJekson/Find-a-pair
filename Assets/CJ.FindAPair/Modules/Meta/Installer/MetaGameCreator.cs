using CJ.FindAPair.Modules.Meta.Themes;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.Meta.Installer
{
    public class MetaGameCreator : MonoBehaviour
    {
        private ThemesSelector _themesSelector;
        
        [Inject]
        public void ConstructMetaGame(ThemesSelector themesSelector)
        {
            _themesSelector = themesSelector;
        }
    }
}