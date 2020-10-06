using UnityEngine;
using TMPro;

namespace CJ.FindAPair.CardTable
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI Text; //TODO ТЕСТ

        public bool IsEmpty { get; set; }

        public int NumberPair { get; set; }

        private void Awake()
        {
            IsEmpty = false;
        }

        private void Start()
        {
            Text.text = NumberPair.ToString();

            if(IsEmpty)
            {
                Text.gameObject.SetActive(false);
            }
        }
    }
}

