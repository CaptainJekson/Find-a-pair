using System;
using CJ.FindAPair.Game;
using CJ.FindAPair.Game.Booster;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CJ.FindAPair.UI
{
    [RequireComponent(typeof(Button))]
    public class UIBooster : MonoBehaviour
    {
        [SerializeField] private BoosterType _boosterType;
        [SerializeField] private TextMeshProUGUI _countText;

        private Button _button;

        public event UnityAction<BoosterType> BoosterButtonPressed;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClickButton);
            SetInfo();
        }

        private void OnEnable()
        {
            GameSaver.OnSaved += SetInfo;
        }

        private void OnDisable()
        {
            GameSaver.OnSaved -= SetInfo;
        }

        private void OnClickButton()
        {
            BoosterButtonPressed?.Invoke(_boosterType);
            SetInfo();
        }

        private void SetInfo()
        {
            if (GameSaver.LoadBooster(_boosterType) <= 0)
                _button.interactable = false;
            
            _countText.SetText(GameSaver.LoadBooster(_boosterType).ToString());
        }
    }
}