using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CutScenes.CutScenes;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class VictoryWindow : Window
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        [SerializeField] private TextMeshProUGUI _coinsValueText;
        [SerializeField] private Transform _coinsParentTransform;

        private UIRoot _uiRoot;
        private LevelCreator _levelCreator;
        private GameWatcher _gameWatcher;
        private ProgressGiftBoxSaver _progressGiftBoxSaver;
        private LevelRewardCutScene _levelRewardCutScene;

        public Transform CoinsParentTransform => _coinsParentTransform;

        [Inject]
        public void Construct(UIRoot uiRoot, LevelCreator levelCreator, GameWatcher gameWatcher, 
            ProgressGiftBoxSaver progressGiftBoxSaver, LevelRewardCutScene levelRewardCutScene)
        {
            _uiRoot = uiRoot;
            _levelCreator = levelCreator;
            _gameWatcher = gameWatcher;
            _progressGiftBoxSaver = progressGiftBoxSaver;
            _levelRewardCutScene = levelRewardCutScene;
        }

        protected override void Init()
        {
            _gameWatcher.ThereWasAVictory += Open;
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
        }
    
        protected override void OnOpen()
        {
            _uiRoot.OpenWindow<GameBlockWindow>();
            _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
            _levelRewardCutScene.Play();
            _progressGiftBoxSaver.SaveProgress();
        }

        protected override void OnClose()
        {
            _uiRoot.CloseWindow<GameBlockWindow>();
        }

        protected override void PlayOpenSound()
        {
            _audioController.StopMusic();
            _audioController.PlaySound(_audioController.AudioClipsCollection.VictorySound);
        }

        protected override void PlayCloseSound()
        {
        }

        private void OnRestartButtonClick()
        {
            _levelCreator.RestartLevel();
            _uiRoot.GetWindow<GameInterfaceWindow>().SetIncomeLockImage();
            Close();
        }
        
        private void OnNextLevelButtonClick()
        {
            _levelCreator.ClearLevel();
            Close();
        }

        public void SetCoinsValue(int value)
        {
            _coinsValueText.SetText(value.ToString());
        }
    }
}