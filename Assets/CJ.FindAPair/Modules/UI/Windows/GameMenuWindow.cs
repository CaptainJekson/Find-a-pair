using CJ.FindAPair.Modules.CoreGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameMenuWindow : Window
{
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    [SerializeField] private Button _restartNoEnergyButton;

    private LevelCreator _levelCreator;
    private ISaver _gameSaver;

    [Inject]
    public void Construct(LevelCreator levelCreator, ISaver gameSaver)
    {
        _levelCreator = levelCreator;
        _gameSaver = gameSaver;
    }
    
    protected override void OnOpen()
    {
        Time.timeScale = 0.0f;
        _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
        ChangeStateRestartNoEnergyButton();
    }

    protected override void OnClose()
    {
        Time.timeScale = 1.0f;
        
        foreach (var card in _levelCreator.Cards)
        {
            card.EnableInteractable();
        }
    }

    private void ChangeStateRestartNoEnergyButton()
    {
        var saveData = _gameSaver.LoadData();
        _restartNoEnergyButton.gameObject.SetActive(saveData.ItemsData.Energy <= 1);
    }
}