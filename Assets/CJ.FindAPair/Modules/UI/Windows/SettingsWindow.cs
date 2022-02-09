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
            _gameSaver.SaveCreated += () => _playerId = $"{_gameSaver.LoadData().UserId.ToString()}";
            
            SetToggles();
        }

        protected override void OnOpen()
        {
            _audioController.PlaySound(_audioController.AudioClipsCollection.WindowOpenSound);
            _copyPlayerIdButton.onClick.AddListener(CopyPlayerId);
            _playerIdText.SetText($"User Id: {_playerId}");
        }

        protected override void OnClose()
        {
            _copyPlayerIdButton.onClick.RemoveListener(CopyPlayerId);
        }
        
        protected override void OnCloseButtonClick()
        {
            _audioController.PlaySound(_audioController.AudioClipsCollection.WindowCloseSound);
        }
        
        public void OnSoundToggleSwitch()
        {
            if (_soundsToggle.isOn == false)
            {
                PlayerPrefs.SetString(PlayerPrefsKeys.SoundsTogglePosition, "Off");
                _audioController.SetSoundState(true);
            }
            else
            {
                PlayerPrefs.SetString(PlayerPrefsKeys.SoundsTogglePosition, "On");
                _audioController.SetSoundState(false);
            }
        }
        
        public void OnMusicToggleSwitch()
        {
            if (_musicToggle.isOn == false)
            {
                PlayerPrefs.SetString(PlayerPrefsKeys.MusicTogglePosition, "Off");
                _audioController.SetMusicState(true);
            }
            else
            {
                PlayerPrefs.SetString(PlayerPrefsKeys.MusicTogglePosition, "On");
                _audioController.SetMusicState(false);
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