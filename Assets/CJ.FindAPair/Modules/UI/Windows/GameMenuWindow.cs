using CJ.FindAPair.Modules.CoreGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameMenuWindow : Window
{
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _restartButton;
    
    private LevelCreator _levelCreator;
    private EnergyHandler _energyHandler;
    
    [Inject]
    public void Construct(LevelCreator levelCreator, EnergyHandler energyHandler)
    {
        _levelCreator = levelCreator;
        _energyHandler = energyHandler;
    }

    protected override void OnOpen()
    {
        _exitButton.onClick.AddListener(_energyHandler.DecreaseScore);
        _restartButton.onClick.AddListener(_energyHandler.DecreaseScore);
        
        Time.timeScale = 0.0f;
        _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
    }

    protected override void OnClose()
    {
        _exitButton.onClick.RemoveListener(_energyHandler.DecreaseScore);
        _restartButton.onClick.RemoveListener(_energyHandler.DecreaseScore);
        
        Time.timeScale = 1.0f;
    }
}