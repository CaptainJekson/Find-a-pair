using UnityEngine;
using TMPro;

namespace CJ.FindAPair.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UIValue : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void SetValue(string value)
        {
            _text.text = value;
        }
    }
}

