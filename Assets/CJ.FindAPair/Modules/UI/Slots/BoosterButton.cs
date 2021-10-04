using CJ.FindAPair.Modules.CoreGames.Booster;
using CJ.FindAPair.Modules.UI.Windows;
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
        private CooldownBar _cooldownBar;
        private BoosterHandler _boosterHandler;
        private BoosterInterfaceWindow _boosterInterfaceWindow;
        private ISaver _gameSaver;

        protected void Start()
        {
            _button = GetComponent<Button>();
            _cooldownBar = GetComponentInChildren<CooldownBar>();
            _boosterInterfaceWindow = GetComponentInParent<BoosterInterfaceWindow>();
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

                if (_boosterType == BoosterType.Detector || _boosterType == BoosterType.Magnet)
                {
                    _boosterInterfaceWindow.CooldownBoosters();
                }
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

        public void TryActivateCooldown(float cooldownTime)
        {
            if (GetBoosterSaveData() > 0)
            {
                _button.interactable = false;
                _cooldownBar.ActivateCooldownAnimation(cooldownTime, MakeButtonAvailable);
            }
            else if (GetBoosterSaveData() <= 0)
            {
                _button.interactable = false;
            }
        }

        private void MakeButtonAvailable()
        {
            _button.interactable = true;
        }

        public BoosterType GetButtonBoosterType()
        {
            return _boosterType;
        }
    }
}