using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.Modules.CutScenes
{
    public class GiftItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _value;
        
        public void SetItem(Sprite icon, int count)
        {
            _icon.sprite = icon;
            _value.SetText($"x{count}");
        }
    }
}