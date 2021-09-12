using CJ.FindAPair.Modules.Service.Save;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

//TODO TEST CLASS!!!
public class TestBooster : MonoBehaviour
{
    private Button _button;
    private GameSaver _gameSaver;
    private UIRoot _uiRoot;

    [Inject]
    public void Construct(UIRoot uiRoot, GameSaver gameSaver)
    {
        _uiRoot = uiRoot;
        _gameSaver = gameSaver;
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(AddBoosters);
    }

    private void AddBoosters()
    {
        _gameSaver.IncreaseNumberValue(1, SaveKeys.Sapper);
        _gameSaver.IncreaseNumberValue(1, SaveKeys.Detector);
        _gameSaver.IncreaseNumberValue(1, SaveKeys.Magnet);

        _uiRoot.GetWindow<BoosterInterfaceWindow>().RefreshButtons();
    }
}