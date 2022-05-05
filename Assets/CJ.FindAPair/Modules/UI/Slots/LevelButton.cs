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
        [SerializeField] private Image _boxIcon;
        [SerializeField] private Sprite _levelHardSprite;
        [SerializeField] private Sprite _levelLockSprite;
        
        private Sprite _levelStandardSprite;
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
        
        public void OpenPreviewWindow()
        {
            var saveData = _gameSaver.LoadData();

            if (saveData.ItemsData.Energy <= 0)
            {
                _uiRoot.OpenWindow<EnergyBoostOfferWindow>();
            }
            else
            {
                _previewLevelWindow.SetData(_levelConfig, _levelCreator);
                _previewLevelWindow.Open();
            }
            
            _uiRoot.OpenWindow<BlockWindow>();
        }

        public void SetLockState()
        {
            _lockIcon.gameObject.SetActive(true);
            _levelNumberText.gameObject.SetActive(false);
            _mainImage.sprite = /*_levelLockSprite*/ _levelStandardSprite; //TODO тут надо как то по другому
        }

        public void SetUnlockState()
        {
            _levelNumberText.gameObject.SetActive(true);
            _mainImage.sprite = /*_levelConfig.IsHard ? _levelHardSprite :*/ _levelStandardSprite; //TODO тут надо как то по другому
            
            var sequence = DOTween.Sequence();
            sequence.Append(_lockIcon.transform.DOMoveY(_lockIcon.transform.position.y - 100.0f, 0.5f))
                .SetEase(Ease.InCirc);
            sequence.AppendCallback(() =>_lockIcon.gameObject.SetActive(false));
        }

        public void SetStandardSprite(Sprite sprite)
        {
            _levelStandardSprite = sprite;
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
            TrySetGiftIcon();

            if (currentLevel >= _levelConfig.LevelNumber || _levelConfig.LevelNumber == 1)
            {
                _levelNumberText.text = _levelConfig.LevelNumber.ToString();
                _mainImage.sprite = /*_levelConfig.IsHard ? _levelHardSprite :*/ _levelStandardSprite; //TODO тут надо как то по другому
                
                SetOpenLevel(true);
            }
            else
            {
                _mainImage.sprite = /*_levelLockSprite*/ _levelStandardSprite; //TODO тут надо как то по другому
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
        
        private void TrySetGiftIcon()
        {
            var completedLevels = _gameSaver.LoadData().CompletedLevels;

            foreach (var completedLevel in completedLevels)
            {
                if (_levelConfig.IsHard == false || completedLevel == _levelConfig.LevelNumber)
                {
                    _boxIcon.gameObject.SetActive(false);
                }
            }
        }
    }
}