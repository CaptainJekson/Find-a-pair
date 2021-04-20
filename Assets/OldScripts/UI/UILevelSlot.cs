using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CJ.FindAPair.CoreGames;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using Doozy.Engine.UI;

namespace CJ.FindAPair.UI
{
    [RequireComponent(typeof(Button))]
    public class UILevelSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelNumberText;

        private LevelConfig _level;
        private LevelCreator _levelCreator;
        private UIPreviewLevel _uIPreviewLevel;
        private Button _button;

        public LevelConfig Level { get => _level; set => _level = value; }
        public LevelCreator LevelCreator { get => _levelCreator; set => _levelCreator = value; }
        public UIPreviewLevel UIPreviewLevel { get => _uIPreviewLevel; set => _uIPreviewLevel = value; }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OpenPreviewScreen);
        }

        public void SetLevelNumberText(int numblerLevel)
        {
            _levelNumberText.text = numblerLevel.ToString();
        }

        private void OpenPreviewScreen()
        {
            _uIPreviewLevel.SetDataLevel(this);
            UIView.ShowView("Main menu", "Preview level screen");
            UIView.ShowView("General", "BlockPanel");
        }
    }
}

