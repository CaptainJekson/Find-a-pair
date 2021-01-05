using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using CJ.FindAPair.Configuration;
using CJ.FindAPair.Constants;

namespace CJ.FindAPair.CardTable
{
    public class Card : MonoBehaviour
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

        public Sprite Shirt
        {
            get => _shirt.sprite;
            set => _shirt.sprite = value;
        }

        public Sprite Face
        {
            get => _face.sprite;
            set => _face.sprite = value;
        }

        public event UnityAction СardOpens;
        public event UnityAction CardClosed;

        private void Awake()
        {
            IsEmpty = false;
            IsShow = false;
            _button.onClick.AddListener(Show);
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
        
        public void Show()
        {
            IsShow = true;
            СardOpens?.Invoke();
        }

        public void Hide()
        {
            IsShow = false;
            IsMatched = false;
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