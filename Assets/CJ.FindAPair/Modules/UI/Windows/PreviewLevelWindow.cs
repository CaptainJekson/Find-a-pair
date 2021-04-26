using System.Linq;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class PreviewLevelWindow : MonoBehaviour
    {
        [SerializeField] private Button _startLevelButton;
        [SerializeField] private TextMeshProUGUI _levelNumberText;
        [SerializeField] private TextMeshProUGUI _quantityOfCardOfPairText;
        [SerializeField] private TextMeshProUGUI _quantityCardsText;
        [SerializeField] private Image _bombIcon;

        private int _quantityCards = 0;
        private LevelCreator _levelCreator;
        private LevelConfig _levelConfig;

        private void Awake()
        {
            _startLevelButton.onClick.AddListener(OnStartLevelButtonClick);
        }

        public void SetData(LevelConfig levelConfig, LevelCreator levelCreator)
        {
            _levelCreator = levelCreator;
            _levelConfig = levelConfig;
            
            foreach (var fieldElement in levelConfig.LevelField.Where(fieldElement => fieldElement))
            {
                ++_quantityCards;
            }

            ShowInfo();
        }

        private void ShowInfo()
        {
            _levelNumberText.SetText(_levelConfig.LevelNumber.ToString());
            _quantityOfCardOfPairText.text = ((int) _levelConfig.QuantityOfCardOfPair).ToString();
            _quantityCardsText.text = _quantityCards.ToString();
            _bombIcon.gameObject.SetActive(_levelConfig.QuantityPairOfBombs > 0);
        }

        private void OnStartLevelButtonClick()
        {
            _levelCreator.CreateLevel(_levelConfig);
        }
    }
}
