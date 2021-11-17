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

        private void CopyPlayerId()
        {
            GUIUtility.systemCopyBuffer = _playerId;
        }
    }
}