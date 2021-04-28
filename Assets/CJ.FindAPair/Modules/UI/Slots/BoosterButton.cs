using CJ.FindAPair.Modules.CoreGames.Booster;
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

        protected void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClickButton);
        }

        public void Init(BoosterHandler boosterHandler)
        {
            _boosterHandler = boosterHandler;
        }

        private void OnClickButton()
        {
            _boosterHandler.BoosterActivationHandler(_boosterType);
        }
    }
}