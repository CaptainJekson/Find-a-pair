using UnityEngine.SceneManagement;

namespace CJ.FindAPair.Modules.UI.Installer
{
    public class UISceneLoader
    {
        public UISceneLoader()
        {
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        }
    }
}