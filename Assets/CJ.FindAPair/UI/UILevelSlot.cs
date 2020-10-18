using CJ.FindAPair.CardTable;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CJ.FindAPair.Configuration;

namespace CJ.FindAPair.UI
{
    [RequireComponent(typeof(Button))]
    public class UILevelSlot : MonoBehaviour
    {
        [SerializeField] private LevelConfig _level;
        [SerializeField] private LevelCreator _levelCreator;
        [SerializeField] private TextMeshProUGUI _levelNumber;

        private Button _button;

        public LevelConfig Level { get => _level; set => _level = value; }
        public LevelCreator LevelCreator { get => _levelCreator; set => _levelCreator = value; }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(CreateLevel);
        }

        public void SetLevelNumberText(int numblerLevel)
        {
            _levelNumber.text = numblerLevel.ToString();
        }

        private void CreateLevel()
        {
            LevelCreator.CreateLevel(Level);
        }
    }
}

