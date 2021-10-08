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
            _cooldownBar = GetComponentInChildren<CooldownBar>();

            _button.onClick.AddListener(OnClickButton);

            SetCounter();

            if (_boosterType == BoosterType.Sapper)
                _boosterInterfaceWindow.TryDisableSapperButton();
        }

        public void Init(BoosterHandler boosterHandler, ISaver gameSaver)
        {
            _button = GetComponent<Button>();
            _boosterInterfaceWindow = GetComponentInParent<BoosterInterfaceWindow>();

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
                    _boosterInterfaceWindow.CooldownBoosters();
                else if (_boosterType == BoosterType.Sapper)
                    MakeButtonUnavailable();
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
                MakeButtonUnavailable();
                _cooldownBar.ActivateCooldownAnimation(cooldownTime, MakeButtonAvailable);
            }
            else if (GetBoosterSaveData() <= 0)
            {
                MakeButtonUnavailable();
            }
        }

        public void MakeButtonAvailable()
        {
            _button.interactable = true;
        }

        public void MakeButtonUnavailable()
        {
            _button.interactable = false;
        }

        public BoosterType GetButtonBoosterType()
        {
            return _boosterType;
        }
    }
}