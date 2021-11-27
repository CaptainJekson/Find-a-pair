using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;
using DG.Tweening;
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
        [SerializeField] private Image _coinIcon;
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
        public Button Button => _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _mainImage = GetComponent<Image>();
            _button.onClick.AddListener(OpenPreviewWindow);
        }
        
        public void OpenPreviewWindow()
        {
            _previewLevelWindow.SetData(_levelConfig, _levelCreator);
            _previewLevelWindow.Open();
            _uiRoot.OpenWindow<BlockWindow>();
        }

        public void SetLockState()
        {
            _lockIcon.gameObject.SetActive(true);
            _levelNumberText.gameObject.SetActive(false);
            _mainImage.sprite = _levelLockSprite;
        }

        public void SetUnlockState()
        {
            _levelNumberText.gameObject.SetActive(true);
            _mainImage.sprite = _levelConfig.IsHard ? _levelHardSprite : _levelStandardSprite;
            
            
            var sequence = DOTween.Sequence();
            sequence.Append(_lockIcon.transform.DOMoveY(_lockIcon.transform.position.y - 100.0f, 0.5f))
                .SetEase(Ease.InCirc);
            sequence.AppendCallback(() =>_lockIcon.gameObject.SetActive(false));
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

            SetIncomeLevel();

            if (currentLevel >= _levelConfig.LevelNumber || _levelConfig.LevelNumber == 1)
            {
                _levelNumberText.text = _levelConfig.LevelNumber.ToString();
                _mainImage.sprite = _levelConfig.IsHard ? _levelHardSprite : _levelStandardSprite;
                
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

        private void SetIncomeLevel()
        {
            var completedLevels = _gameSaver.LoadData().CompletedLevels;

            foreach (var completedLevel in completedLevels)
            {
                if (completedLevel == _levelConfig.LevelNumber)
                {
                    _coinIcon.gameObject.SetActive(false);    
                }
            }
        }
    }
}

