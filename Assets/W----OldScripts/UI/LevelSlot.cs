using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Doozy.Engine.UI;

namespace CJ.FindAPair.UI
{
    [RequireComponent(typeof(Button))]
    public class LevelSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelNumberText;

        private LevelConfig _level;
        private LevelCreator _levelCreator;
        private PreviewLevelWindow _uIPreviewLevelWindow;
        private Button _button;

        public LevelConfig Level { get => _level; set => _level = value; }
        public LevelCreator LevelCreator { get => _levelCreator; set => _levelCreator = value; }
        public PreviewLevelWindow PreviewLevelWindow { get => _uIPreviewLevelWindow; set => _uIPreviewLevelWindow = value; }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OpenPreviewWindow);
        }

        public void SetLevelNumberText(int numblerLevel)
        {
            _levelNumberText.text = numblerLevel.ToString();
        }

        private void OpenPreviewWindow()
        {
            _uIPreviewLevelWindow.SetDataLevel(this);
            UIView.ShowView("Main menu", "Preview level screen");
            UIView.ShowView("General", "BlockPanel");
        }
    }
}

