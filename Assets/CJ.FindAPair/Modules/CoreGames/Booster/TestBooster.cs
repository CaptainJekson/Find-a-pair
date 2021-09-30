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
    private ISaver _gameSaver;
    private UIRoot _uiRoot;

    [Inject]
    public void Construct(UIRoot uiRoot, ISaver gameSaver)
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
        var saveData = _gameSaver.LoadData();
        saveData.ItemsData.DetectorBooster += 1;
        saveData.ItemsData.MagnetBooster += 1;
        saveData.ItemsData.SapperBooster += 1;
        
        _gameSaver.SaveData(saveData); 
        
        _uiRoot.GetWindow<BoosterInterfaceWindow>().RefreshButtons();
    }
}