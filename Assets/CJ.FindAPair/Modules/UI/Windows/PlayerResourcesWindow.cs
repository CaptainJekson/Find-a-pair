using CJ.FindAPair.Modules.Service.Save;
using TMPro;
using UnityEngine;
using Zenject;

public class PlayerResourcesWindow : Window
{
    [SerializeField] private TextMeshProUGUI _goldValueText;

    private GameSaver _gameSaver;

    [Inject]
    public void Construct(GameSaver gameSaver)
    {
        _gameSaver = gameSaver;
    }

    protected override void OnOpen()
    {
        _goldValueText.SetText(_gameSaver.ReadNumberValue(SaveKeys.Coins).ToString());
    }
}