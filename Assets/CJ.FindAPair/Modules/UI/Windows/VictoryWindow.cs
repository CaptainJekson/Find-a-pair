using System.Linq;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class VictoryWindow : Window
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    
    private LevelCreator _levelCreator;
    private GameWatcher _gameWatcher;
    private LevelConfigCollection _levelConfigCollection;
    
    [Inject]
    public void Construct(LevelCreator levelCreator, GameWatcher gameWatcher, 
        LevelConfigCollection levelConfigCollection)
    {
        _levelCreator = levelCreator;
        _gameWatcher = gameWatcher;
        _levelConfigCollection = levelConfigCollection;
    }

    protected override void Init()
    {
        _gameWatcher.ThereWasAVictory += Open;
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
    }
    
    protected override void OnOpen()
    {
        _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
    }
    
    private void OnRestartButtonClick()
    {
        _levelCreator.RestartLevel();
        Close();
        Debug.LogError("OnRestartButtonClick");
    }

    private void OnExitButtonClick()
    {
        _levelCreator.ClearLevel();
        Close();
    }

    private void OnNextLevelButtonClick()
    {
        LevelConfig nextLevel;
        var currentLevelNumber = _levelCreator.LevelConfig.LevelNumber;

        ++currentLevelNumber;
        
        try
        {
            nextLevel = _levelConfigCollection.Levels.First(item => item.LevelNumber == 
                                                                    currentLevelNumber);
        }
        catch
        {
            nextLevel = _levelConfigCollection.Levels.First(item => item.LevelNumber == 1);
        }

        _levelCreator.ClearLevel();
        _levelCreator.CreateLevel(nextLevel);
        
        Close();
    }
}
