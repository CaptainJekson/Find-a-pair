using System.Linq;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.UI.Installer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class PreviewLevelWindow : Window
    {
        [SerializeField] private Button _startLevelButton;
        [SerializeField] private TextMeshProUGUI _levelNumberText;
        [SerializeField] private TextMeshProUGUI _quantityOfCardOfPairText;
        [SerializeField] private TextMeshProUGUI _quantityCardsText;
        [SerializeField] private Image _bombIcon;

        private int _quantityCards = 0;
        private LevelCreator _levelCreator;
        private LevelConfig _levelConfig;
        private BlockWindow _blockWindow;
        private AudioController _audioController;

        [Inject]
        private void Construct(UIRoot uiRoot, AudioController audioController)
        {
            _blockWindow = uiRoot.GetWindow<BlockWindow>();
            _audioController = audioController;
        }
        
        protected override void Init()
        {
            _startLevelButton.onClick.AddListener(OnStartLevelButtonClick);
        }

        protected override void OnOpen()
        {
            _audioController.PlaySound(_audioController.AudioClipsCollection.WindowOpenSound);
            _blockWindow.SetOpenWindow(this);
        }

        protected override void OnCloseButtonClick()
        {
            _blockWindow.Close();
            base.OnCloseButtonClick();
            _audioController.PlaySound(_audioController.AudioClipsCollection.WindowCloseSound);
        }

        public void SetData(LevelConfig levelConfig, LevelCreator levelCreator)
        {
            _quantityCards = 0;
            _levelCreator = levelCreator;
            _levelConfig = levelConfig;
            
            foreach (var fieldElement in levelConfig.LevelField.Where(fieldElement => fieldElement))
            {
                ++_quantityCards;
            }

            ShowInfo();
        }

        private void ShowInfo()
        {
            _levelNumberText.SetText(_levelConfig.LevelNumber.ToString());
            _quantityOfCardOfPairText.text = ((int) _levelConfig.QuantityOfCardOfPair).ToString();
            _quantityCardsText.text = _quantityCards.ToString();
            _bombIcon.gameObject.SetActive(_levelConfig.QuantityPairOfBombs > 0);
        }

        private void OnStartLevelButtonClick()
        {
            _levelCreator.CreateLevel(_levelConfig);
        }
    }
}
