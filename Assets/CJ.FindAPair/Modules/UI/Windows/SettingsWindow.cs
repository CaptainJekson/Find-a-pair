using CJ.FindAPair.Modules.UI.Windows.Base;
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

        [Inject]
        public void Construct(ISaver gameSaver)
        {
            _gameSaver = gameSaver;
        }

        protected override void Init()
        {
            _playerId = $"{_gameSaver.LoadData().UserId.ToString()}";
            _gameSaver.SaveCreated += () => _playerId = $"{_gameSaver.LoadData().UserId.ToString()}";
        }

        protected override void OnOpen()
        {
            _soundsToggle.isOn = _audioController.IsSoundsMute;
            _musicToggle.isOn = _audioController.IsMusicMute;

            _copyPlayerIdButton.onClick.AddListener(CopyPlayerId);
            _playerIdText.SetText($"User Id: {_playerId}");
        }

        protected override void OnClose()
        {
            _copyPlayerIdButton.onClick.RemoveListener(CopyPlayerId);
        }

        public void OnSoundToggleSwitch()
        {
            _audioController.SetSoundsState(_soundsToggle.isOn);
        }
        
        public void OnMusicToggleSwitch()
        {
            _audioController.SetMusicState(_musicToggle.isOn);
        }

        private void CopyPlayerId()
        {
            GUIUtility.systemCopyBuffer = _playerId;
        }
    }
}