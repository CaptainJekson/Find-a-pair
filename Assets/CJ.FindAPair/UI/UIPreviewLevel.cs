using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.UI
{
    public class UIPreviewLevel : MonoBehaviour
    {
        [SerializeField] private Button _startLevelButton;
        [SerializeField] private TextMeshProUGUI _levelNumberText;
        [SerializeField] private TextMeshProUGUI _quantityOfCardOfPair;
        [SerializeField] private TextMeshProUGUI _triesText;
        [SerializeField] private TextMeshProUGUI _time;
        [SerializeField] private Image _bombIcon; 

        private UILevelSlot _uILevelSlot;

        private void OnEnable()
        {
            _startLevelButton.onClick.AddListener(StartLevel);
        }

        private void OnDisable()
        {
            _startLevelButton.onClick.RemoveListener(StartLevel);
        }

        public void SetDataLevel(UILevelSlot uILevelSlot)
        {
            _uILevelSlot = uILevelSlot;

            SetDataView();
        }

        public void SetDataView()
        {
            _levelNumberText.text = _uILevelSlot.Level.LevelNumber.ToString();
            _quantityOfCardOfPair.text = ((int)_uILevelSlot.Level.QuantityOfCardOfPair).ToString();
            _triesText.text = _uILevelSlot.Level.Tries.ToString();
            _time.text = TimeConverer(_uILevelSlot.Level.Time);
            _bombIcon.gameObject.SetActive(_uILevelSlot.Level.QuantityPairOfBombs > 0);
        }

        private string TimeConverer(int secondTime) //TODO повторяется в GameWatcher
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
