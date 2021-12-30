using CJ.FindAPair.Modules.CoreGames;
using TMPro;
using UnityEngine;

public class GameResourceItem : MonoBehaviour
{
    [SerializeField] private ItemTypes _itemType;

    public ItemTypes ItemType => _itemType;

    public void SetValue(string valueText)
    {
        GetComponentInChildren<TextMeshProUGUI>().SetText(valueText);
    }
}