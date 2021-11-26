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
    private EnergyCooldownHandler _energyCooldownHandler;
    
    [Inject]
    public void Construct(LevelCreator levelCreator, EnergyCooldownHandler energyCooldownHandler)
    {
        _levelCreator = levelCreator;
        _energyCooldownHandler = energyCooldownHandler;
    }

    protected override void OnOpen()
    {
        _exitButton.onClick.AddListener(_energyCooldownHandler.TryDecreaseScore);
        _restartButton.onClick.AddListener(_energyCooldownHandler.TryDecreaseScore);
        
        Time.timeScale = 0.0f;
        _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
    }

    protected override void OnClose()
    {
        _exitButton.onClick.RemoveListener(_energyCooldownHandler.TryDecreaseScore);
        _restartButton.onClick.RemoveListener(_energyCooldownHandler.TryDecreaseScore);
        
        Time.timeScale = 1.0f;
    }
}