using CJ.FindAPair.Modules.Meta.Themes;
using CJ.FindAPair.Modules.UI.Installer;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class ThemeWindow : Window
    {
        [SerializeField] private Button _testFoodButton;
        [SerializeField] private Button _testAnimalsButton;
        
        private BlockWindow _blockWindow;
        private ThemesSelector _themesSelector;
        
        [Inject]
        private void Construct(UIRoot uiRoot, ThemesSelector themesSelector)
        {
            _blockWindow = uiRoot.GetWindow<BlockWindow>();
            _themesSelector = themesSelector;
        }

        protected override void Init()
        {
            _testFoodButton.onClick.AddListener(() => _themesSelector.ChangeTheme("food")); //TODO test
            _testAnimalsButton.onClick.AddListener(() => _themesSelector.ChangeTheme("animals")); //TODO test
        }
        
        protected override void OnCloseButtonClick()
        {
            _blockWindow.Close();
            base.OnCloseButtonClick();
        }
    }
}