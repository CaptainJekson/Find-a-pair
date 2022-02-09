using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CutScenes.CutScenes;
using CJ.FindAPair.Modules.UI.Installer;
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
        private ProgressSaver _progressSaver;
        private LevelRewardCutScene _levelRewardCutScene;
        private AudioController _audioController;

        public Transform CoinsParentTransform => _coinsParentTransform;

        [Inject]
        public void Construct(UIRoot uiRoot, LevelCreator levelCreator, GameWatcher gameWatcher, 
            ProgressSaver progressSaver, LevelRewardCutScene levelRewardCutScene, AudioController audioController)
        {
            _uiRoot = uiRoot;
            _levelCreator = levelCreator;
            _gameWatcher = gameWatcher;
            _progressSaver = progressSaver;
            _levelRewardCutScene = levelRewardCutScene;
            _audioController = audioController;
        }

        protected override void Init()
        {
            _gameWatcher.ThereWasAVictory += Open;
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
        }
    
        protected override void OnOpen()
        {
            _audioController.StopMusic();
            _audioController.PlaySound(_audioController.AudioClipsCollection.VictorySound);
            _uiRoot.OpenWindow<GameBlockWindow>();
            _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
            _levelRewardCutScene.Play();
            _progressSaver.SaveProgress();
        }

        protected override void OnClose()
        {
            _uiRoot.CloseWindow<GameBlockWindow>();
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