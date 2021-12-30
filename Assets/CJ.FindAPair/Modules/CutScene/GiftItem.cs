using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.Modules.CutScene
{
    public class GiftItem : MonoBehaviour
    {
        public void SetItem(Sprite icon, int count)
        {
            GetComponent<Image>().sprite = icon;
            GetComponentInChildren<TextMeshProUGUI>().SetText($"x{count}");
        }
    }
}