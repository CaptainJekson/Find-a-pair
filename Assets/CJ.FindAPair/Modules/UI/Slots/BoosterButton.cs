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
        private GameSaver _gameSaver;

        protected void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClickButton);
            
            SetCounter();
        }

        public void Init(BoosterHandler boosterHandler, GameSaver gameSaver)
        {
            _boosterHandler = boosterHandler;
            _gameSaver = gameSaver;
        }

        private void OnClickButton()
        {
            _boosterHandler.BoosterActivationHandler(_boosterType);

            if (_gameSaver.DecreaseNumberValueIfPossible(1, GetBoosterSaveKey()))
            {
                SetCounter();
            }
        }
        
        public void SetCounter()
        {
            var boosterCount = _gameSaver.ReadNumberValue(GetBoosterSaveKey());
            _countText.SetText(boosterCount.ToString());
            _button.interactable = boosterCount > 0;
        }

        private string GetBoosterSaveKey()
        {
            switch (_boosterType)
            {
                case BoosterType.Electroshock:
                    return SaveKeys.Electroshock;
                case BoosterType.Sapper:
                    return SaveKeys.Sapper;
                case BoosterType.MagicEye:
                    return SaveKeys.MagicEye;
            }

            return null;
        }
    }
}