using System.Diagnostics;
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
        [SerializeField] private bool _canCooldown;
        [SerializeField] private BoosterType _boosterType;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private BoosterInterfaceWindow _boosterInterfaceWindow;
        [SerializeField] private CooldownBar _cooldownBar;

        private Button _button;
        private BoosterHandler _boosterHandler;
        private ISaver _gameSaver;

        public bool CanCooldown => _canCooldown;

        protected void Start()
        {
            _button.onClick.AddListener(OnClickButton);

            SetCounter();
        }

        public void Init(BoosterHandler boosterHandler, ISaver gameSaver)
        {
            _button = GetComponent<Button>();

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
            }
        }

        public void SetCounter()
        {
            var boosterCount = GetBoosterSaveData();
            _countText.SetText(boosterCount.ToString());
            
            _button.interactable = boosterCount > 0;
            
            if (_boosterType == BoosterType.Sapper)
                _boosterInterfaceWindow.TryDisableSapperButton();
        }

        public int GetBoosterSaveData()
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
        }
        
        public void MakeButtonAvailable()
        {
            _button.interactable = true;
        }

        public void MakeButtonUnavailable()
        {
            _button.interactable = false;
        }
    }
}