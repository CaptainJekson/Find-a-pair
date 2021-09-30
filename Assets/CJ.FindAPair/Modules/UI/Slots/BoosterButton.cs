using CJ.FindAPair.Modules.CoreGames.Booster;
using CJ.FindAPair.Modules.Service.Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.Modules.UI.Slots
{
    [RequireComponent(typeof(Button))]
    public class BoosterButton : MonoBehaviour
    {
        [SerializeField] private BoosterType _boosterType;
        [SerializeField] private TextMeshProUGUI _countText;

        private Button _button;
        private BoosterHandler _boosterHandler;
        private ISaver _gameSaver;

        protected void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClickButton);

            SetCounter();
        }

        public void Init(BoosterHandler boosterHandler, ISaver gameSaver)
        {
            _boosterHandler = boosterHandler;
            _gameSaver = gameSaver;
        }

        private void OnClickButton()
        {
            _boosterHandler.BoosterActivationHandler(_boosterType);

            if (DecreaseBoosterIfPossible(1))
            {
                SetCounter();
            }
        }

        public void SetCounter()
        {
            var boosterCount = GetBoosterSaveData();
            _countText.SetText(boosterCount.ToString());
            _button.interactable = boosterCount > 0;
        }

        private int GetBoosterSaveData()
        {
            return _boosterType switch
            {
                BoosterType.Magnet => _gameSaver.LoadData().ItemsData.MagnetBooster,
                BoosterType.Sapper => _gameSaver.LoadData().ItemsData.SapperBooster,
                BoosterType.Detector => _gameSaver.LoadData().ItemsData.DetectorBooster,
                _ => 0
            };
        }

        private bool DecreaseBoosterIfPossible(int value)
        {
            var gameSave = _gameSaver.LoadData();

            var result = _boosterType switch
            {
                BoosterType.Detector => gameSave.DecreaseDetectorBoosterIfPossible(value),
                BoosterType.Magnet => gameSave.DecreaseMagnetBoosterIfPossible(value),
                BoosterType.Sapper => gameSave.DecreaseSapperBoosterIfPossible(value),
                _ => false
            };

            _gameSaver.SaveData(gameSave);
            return result;
        }
    }
}