using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Installer
{
    public class UICreator : MonoBehaviour
    {
        private UISceneLoader _uiSceneLoader;
        
        [Inject]
        public void ConstructUI(UISceneLoader uiSceneLoader)
        {
            _uiSceneLoader = uiSceneLoader;
        }
    }
}