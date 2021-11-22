using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.Modules.UI.Slots
{
    [RequireComponent(typeof(Button),typeof(Image))]
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelNumberText;
        [SerializeField] private Image _lockIcon;
        [SerializeField] private Sprite _levelStandardSprite;
        [SerializeField] private Sprite _levelHardSprite;
        [SerializeField] private Sprite _levelLockSprite;

        private LevelConfig _levelConfig;
        private LevelCreator _levelCreator;
        private PreviewLevelWindow _previewLevelWindow;
        private UIRoot _uiRoot;
        private ISaver _gameSaver;
        
        private Button _button;
        private Image _mainImage;
        private int _levelNumber;

        public int LevelNumber => _levelConfig.LevelNumber;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _mainImage = GetComponent<Image>();
            _button.onClick.AddListener(OpenPreviewWindow);
        }

        public void SetData(LevelConfig levelConfig, LevelCreator levelCreator, UIRoot uiRoot, ISaver gameSaver)
        {
            gameObject.SetActive(true);
            
            _levelConfig = levelConfig;
            _levelCreator = levelCreator;
            _uiRoot = uiRoot;
            _gameSaver = gameSaver;

            _previewLevelWindow = uiRoot.GetWindow<PreviewLevelWindow>();
        }

        public void SetStateButton()
        {
            var currentLevel = _gameSaver.LoadData().CurrentLevel;

            if (currentLevel >= _levelConfig.LevelNumber || _levelConfig.LevelNumber == 1)
            {
                _levelNumberText.text = _levelConfig.LevelNumber.ToString();
                _mainImage.sprite = _levelStandardSprite;
                
                if (_levelConfig.IsHard)
                    _mainImage.sprite = _levelHardSprite;
                
                SetOpenLevel(true);
            }
            else
            {
                _mainImage.sprite = _levelLockSprite;
                SetOpenLevel(false);
            }
        }

        private void SetOpenLevel(bool isOpen)
        {
            _levelNumberText.gameObject.SetActive(isOpen);
            _lockIcon.gameObject.SetActive(!isOpen);
            _button.interactable = isOpen;
        }

        private void OpenPreviewWindow()
        {
            _previewLevelWindow.SetData(_levelConfig, _levelCreator);
            _previewLevelWindow.Open();
            _uiRoot.OpenWindow<BlockWindow>();
        }
    }
}

