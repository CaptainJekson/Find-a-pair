using CJ.FindAPair.Modules.CoreGames;
using TMPro;
using UnityEngine;
using Zenject;

public class GameMenuWindow : Window
{
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    
    private LevelCreator _levelCreator;
    
    [Inject]
    public void Construct(LevelCreator levelCreator)
    {
        _levelCreator = levelCreator;
    }

    protected override void OnOpen()
    {
        Time.timeScale = 0.0f;
        _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
    }

    protected override void OnClose()
    {
        Time.timeScale = 1.0f;
    }
}
