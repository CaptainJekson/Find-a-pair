using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.UI.Windows.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class GameMenuWindow : Window
    {
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        [SerializeField] private Button _restartNoEnergyButton;
        [SerializeField] private Toggle _soundsToggle;
        [SerializeField] private Toggle _musicToggle;
    
        private LevelCreator _levelCreator;
        private ISaver _gameSaver;

        [Inject]
        public void Construct(LevelCreator levelCreator, ISaver gameSaver)
        {
            _levelCreator = levelCreator;
            _gameSaver = gameSaver;
        }
    
        protected override void OnOpen()
        {
            Time.timeScale = 0.0f;
        
            _soundsToggle.isOn = !_audioController.IsSoundsMute;
            _musicToggle.isOn = !_audioController.IsMusicMute;
            
            base.OnOpen();

            _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
            ChangeStateRestartNoEnergyButton();
        }

        protected override void OnClose()
        {
            Time.timeScale = 1.0f;
        }

        public void OnSoundToggleSwitch()
        {
            _audioController.SetSoundsState(!_soundsToggle.isOn);
        }
        
        public void OnMusicToggleSwitch()
        {
            _audioController.SetMusicState(!_musicToggle.isOn);
        }

        private void ChangeStateRestartNoEnergyButton()
        {
            var saveData = _gameSaver.LoadData();
            _restartNoEnergyButton.gameObject.SetActive(saveData.ItemsData.Energy <= 1);
        }
    }
}