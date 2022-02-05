using CJ.FindAPair.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class SettingsWindow : Window
    {
        [SerializeField] private Button _copyPlayerIdButton;
        [SerializeField] private TextMeshProUGUI _playerIdText;
        [SerializeField] private Toggle _soundsToggle;
        [SerializeField] private Toggle _musicToggle;

        private string _playerId;
        private ISaver _gameSaver;
        private AudioController _audioController;

        [Inject]
        public void Construct(ISaver gameSaver, AudioController audioController)
        {
            _gameSaver = gameSaver;
            _audioController = audioController;
        }

        protected override void Init()
        {
            _playerId = $"{_gameSaver.LoadData().UserId.ToString()}";
            SetToggles();
            _gameSaver.SaveCreated += () => _playerId = $"{_gameSaver.LoadData().UserId.ToString()}";
        }

        protected override void OnOpen()
        {
            _copyPlayerIdButton.onClick.AddListener(CopyPlayerId);
            _playerIdText.SetText($"User Id: {_playerId}");
        }

        protected override void OnClose()
        {
            _copyPlayerIdButton.onClick.RemoveListener(CopyPlayerId);
        }
        
        public void OnSoundToggleSwitch()
        {
            if (_soundsToggle.isOn == false)
            {
                PlayerPrefs.SetString(PlayerPrefsKeys.SoundsTogglePosition, "Off");
                _audioController.SwitchAudiosCondition(false, true);
            }
            else
            {
                PlayerPrefs.SetString(PlayerPrefsKeys.SoundsTogglePosition, "On");
                _audioController.SwitchAudiosCondition(false, false);
            }
        }
        
        public void OnMusicToggleSwitch()
        {
            if (_musicToggle.isOn == false)
            {
                PlayerPrefs.SetString(PlayerPrefsKeys.MusicTogglePosition, "Off");
                _audioController.SwitchAudiosCondition(true, true);
            }
            else
            {
                PlayerPrefs.SetString(PlayerPrefsKeys.MusicTogglePosition, "On");
                _audioController.SwitchAudiosCondition(true, false);
            }
        }

        private void CopyPlayerId()
        {
            GUIUtility.systemCopyBuffer = _playerId;
        }

        private void SetToggles()
        {
            if (PlayerPrefs.GetString(PlayerPrefsKeys.SoundsTogglePosition) != "Off")
                _soundsToggle.isOn = true;
            else
                _soundsToggle.isOn = false;

            if (PlayerPrefs.GetString(PlayerPrefsKeys.MusicTogglePosition) != "Off")
                _musicToggle.isOn = true;
            else
                _musicToggle.isOn = false;
        }
    }
}