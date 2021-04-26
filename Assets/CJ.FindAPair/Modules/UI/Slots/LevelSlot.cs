using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.Modules.UI.Slots
{
    [RequireComponent(typeof(Button))]
    public class LevelSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelNumberText;

        private LevelConfig _levelConfig;
        private LevelCreator _levelCreator;
        private PreviewLevelWindow _previewLevelWindow;
        private UIRoot _uiRoot;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OpenPreviewWindow);
        }

        public void SetData(LevelConfig levelConfig, LevelCreator levelCreator, UIRoot uiRoot)
        {
            _levelConfig = levelConfig;
            _levelCreator = levelCreator;
            _previewLevelWindow = uiRoot.GetWindow<PreviewLevelWindow>();
            _uiRoot = uiRoot;

            _levelNumberText.text = _levelConfig.LevelNumber.ToString();
        }

        private void OpenPreviewWindow()
        {
            _previewLevelWindow.SetData(_levelConfig, _levelCreator);
            _previewLevelWindow.Open();
            _uiRoot.OpenWindow<MainMenuBlockWindow>();
        }
    }
}

