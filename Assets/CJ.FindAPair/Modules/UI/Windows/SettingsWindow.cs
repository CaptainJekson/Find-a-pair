using TMPro;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class SettingsWindow : Window
    {
        [SerializeField] private TextMeshProUGUI _playerIdText;

        private ISaver _gameSaver;

        [Inject]
        public void Construct(ISaver gameSaver)
        {
            _gameSaver = gameSaver;
        }

        protected override void OnOpen()
        {
            _playerIdText.SetText($"User Id: {_gameSaver.LoadData().UserId.ToString()}");
        }
    }
}