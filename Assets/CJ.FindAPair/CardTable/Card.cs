using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

        private void Awake()
        {
            IsEmpty = false;
        }

        private void Start()
        {
            _text.text = NumberPair.ToString();

            if (IsEmpty)
            {
                MakeEmpty();
            }
        }

        private void Show()
        {

        }

        private void Hide()
        {

        }

        private void MakeEmpty()
        {
            _shirt.enabled = false;
            _face.enabled = false;
            _text.enabled = false;
        }
    }
}

