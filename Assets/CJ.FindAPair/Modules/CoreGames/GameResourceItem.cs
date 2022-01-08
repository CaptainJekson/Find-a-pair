using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Utility;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class GameResourceItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemValueText;
    [SerializeField] private ItemTypes _type;

    private ISaver _gameSaver;
    private LevelCreator _levelCreator;
    
    public ItemTypes Type => _type;

    [Inject]
    public void Construct(ISaver gameSaver, LevelCreator levelCreator)
    {
        _gameSaver = gameSaver;
        _levelCreator = levelCreator;
    }

    private void OnEnable()
    {
        SetItemValue();
    }

    public void SmoothChangeValue(int endValue, float duration, Ease ease)
    {
        _itemValueText.ChangeOfNumericValueForText(int.Parse(_itemValueText.text), endValue, duration, ease);
    }

    private void SetItemValue()
    {
        var saveData = _gameSaver.LoadData().ItemsData;
        
        switch (_type)
        {
            case ItemTypes.Energy:
                _itemValueText.SetText(saveData.Energy.ToString());
                break;
            case ItemTypes.Diamond:
                _itemValueText.SetText(saveData.Diamond.ToString());
                break;
            case ItemTypes.Coin:
                _itemValueText.SetText(saveData.Coins.ToString());
                break;
            case ItemTypes.DetectorBooster:
                _itemValueText.SetText(saveData.DetectorBooster.ToString());
                break;
            case ItemTypes.MagnetBooster:
                _itemValueText.SetText(saveData.MagnetBooster.ToString());
                break;
            case ItemTypes.SapperBooster:
                _itemValueText.SetText(saveData.SapperBooster.ToString());
                break; 
        }
        
        TrySetStockValue(saveData);
    }

    private void TrySetStockValue(ItemsData saveData)
    {
        var itemsCollection = _levelCreator.LevelConfig.RewardItemsCollection.Items;
        
        for (int j = 0; j < itemsCollection.Count; j++)
        {
            if (_type == itemsCollection[j].Type)
            {
                switch (_type)
                {
                    case ItemTypes.Energy:
                        _itemValueText.SetText((saveData.Energy - itemsCollection[j].Count).ToString());
                        break;
                    case ItemTypes.Diamond:
                        _itemValueText.SetText((saveData.Diamond - itemsCollection[j].Count).ToString());
                        break;
                    case ItemTypes.Coin:
                        _itemValueText.SetText((saveData.Coins - itemsCollection[j].Count).ToString());
                        break;
                    case ItemTypes.DetectorBooster:
                        _itemValueText.SetText((saveData.DetectorBooster - itemsCollection[j].Count).ToString());
                        break;
                    case ItemTypes.MagnetBooster:
                        _itemValueText.SetText((saveData.MagnetBooster - itemsCollection[j].Count).ToString());
                        break;
                    case ItemTypes.SapperBooster:
                        _itemValueText.SetText((saveData.SapperBooster - itemsCollection[j].Count).ToString());
                        break; 
                }

                break;
            }
        }
    }
}