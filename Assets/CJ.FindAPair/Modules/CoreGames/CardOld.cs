﻿using System.Collections;
using CJ.FindAPair.Modules.CoreGames.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CJ.FindAPair.Modules.CoreGames
{
    public class CardOld : MonoBehaviour
    {
        [SerializeField] private GameSettingsConfig _gameSettingsConfig;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _button;
        [SerializeField] private Image _shirt;
        [SerializeField] private Image _face;

        public bool IsEmpty { get; set; }
        public bool IsShow { get; set; }
        public bool IsMatched { get; set; }
        public int NumberPair { get; set; }

        public event UnityAction СardOpens;
        public event UnityAction CardClosed;
        public event UnityAction CardOpensForAnimation;
        public event UnityAction CardClosedForAnimation;

        private void Awake()
        {
            IsEmpty = false;
            IsShow = false;
            _button.onClick.AddListener(() => Show());
        }

        private void Start()
        {
            SetNumberText();

            if (IsEmpty)
            {
                MakeEmpty();
            }

            StartCoroutine(DelayStartHide());
        }

        public void SetNumberText()
        {
            _text.SetText(NumberPair.ToString());
        }
        
        public void Show(bool isNotEventCall = false)
        {
            IsShow = true;
            CardOpensForAnimation?.Invoke();
            
            if(isNotEventCall) return;
            СardOpens?.Invoke();
        }

        public void Hide(bool isNotEventCall = false)
        {
            IsShow = false;
            IsMatched = false;
            CardClosedForAnimation?.Invoke();
            
            if(isNotEventCall) return;
            CardClosed?.Invoke();
        }
        
        public void DelayHide()
        {
            StartCoroutine(DelayHide(_gameSettingsConfig.DelayTimeHide));
        }

        private void MakeEmpty()
        {
            _shirt.enabled = false;
            _face.enabled = false;
            _text.enabled = false;
        }

        private IEnumerator DelayHide(float time)
        {
            yield return new WaitForSeconds(time);
            Hide();
        }

        private IEnumerator DelayStartHide()
        {
            yield return new WaitForSeconds(_gameSettingsConfig.StartTimeShow);
            Hide();
        }
    }
}