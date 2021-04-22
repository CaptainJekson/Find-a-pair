using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.UI
{
    public class PreviewLevelWindow : MonoBehaviour
    {
        [SerializeField] private Button _startLevelButton;
        [SerializeField] private TextMeshProUGUI _levelNumberText;
        [SerializeField] private TextMeshProUGUI _quantityOfCardOfPairText;
        [SerializeField] private TextMeshProUGUI _quantityCardsText;
        [SerializeField] private Image _bombIcon;

        private LevelSlot _uILevelSlot;
        private int _quantityCards = 0;

        private void OnEnable()
        {
            _startLevelButton.onClick.AddListener(StartLevel);
        }

        private void OnDisable()
        {
            _startLevelButton.onClick.RemoveListener(StartLevel);
        }

        public void SetDataLevel(LevelSlot uILevelSlot)
        {
            _uILevelSlot = uILevelSlot;

            InitQuantityCards();
            SetDataView();
            _quantityCards = 0;
        }

        public void SetDataView()
        {
            _levelNumberText.text = _uILevelSlot.Level.LevelNumber.ToString();
            _quantityOfCardOfPairText.text = ((int)_uILevelSlot.Level.QuantityOfCardOfPair).ToString();
            _quantityCardsText.text = _quantityCards.ToString();
            _bombIcon.gameObject.SetActive(_uILevelSlot.Level.QuantityPairOfBombs > 0);
        }

        private void InitQuantityCards()
        {
            foreach (var fieldElement in _uILevelSlot.Level.LevelField)
            {
                if (fieldElement == true)
                {
                    ++_quantityCards;
                }
            }
        }

        private string TimeConverter(int secondTime) //TODO повторяется в GameWatcher
        {
            TimeSpan time = TimeSpan.FromSeconds(secondTime);

            return time.ToString(@"mm\:ss");
        }

        private void StartLevel()
        {
            _uILevelSlot.LevelCreator.CreateLevel(_uILevelSlot.Level);
        }
    }
}
