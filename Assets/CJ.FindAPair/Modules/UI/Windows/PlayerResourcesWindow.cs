using CJ.FindAPair.Modules.Service.Save;
using TMPro;
using UnityEngine;
using Zenject;

public class PlayerResourcesWindow : Window
{
    [SerializeField] private TextMeshProUGUI _goldValueText;

    private ISaver _gameSaver;

    [Inject]
    public void Construct(ISaver gameSaver)
    {
        _gameSaver = gameSaver;
    }

    protected override void OnOpen()
    {
        var saveData = _gameSaver.LoadData();
        
        _goldValueText.SetText(saveData.ItemsData.Coins.ToString()); 
    }
}