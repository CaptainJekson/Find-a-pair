using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

namespace CJ.FindAPair.CardTable
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text; //TODO ТЕСТ
        [SerializeField] private Button _button;
        [SerializeField] private Image _shirt;
        [SerializeField] private Image _face;

        public bool IsEmpty { get; set; }
        public bool IsShow { get; set; }
        public int NumberPair { get; set; }
        public Image Shirt { get => _shirt; set => _shirt = value; }
        public Image Face { get => _face; set => _face = value; }

        public event UnityAction СardOpens;
        public event UnityAction CardClosed;

        private void Awake()
        {
            IsEmpty = false;
            _button.onClick.AddListener(Show);
        }

        private void Start()
        {
            _text.text = NumberPair.ToString();

            if (IsEmpty)
            {
                MakeEmpty();
            }

            StartCoroutine(DelayHide());
        }

        private void Show()
        {
            СardOpens?.Invoke();
        }

        private void Hide()
        {
            CardClosed?.Invoke();
        }

        private void MakeEmpty()
        {
            _shirt.enabled = false;
            _face.enabled = false;
            _text.enabled = false;
        }

        private IEnumerator DelayHide()
        {
            yield return new WaitForSeconds(2.0f);  //TODO

            Hide();
        }
    }
}

