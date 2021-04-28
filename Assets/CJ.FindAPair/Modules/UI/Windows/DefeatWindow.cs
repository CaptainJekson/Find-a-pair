using CJ.FindAPair.Modules.CoreGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DefeatWindow : Window //TODO реализовать кнопку выход
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    
    private LevelCreator _levelCreator;
    private GameWatcher _gameWatcher;
    
    [Inject]
    public void Construct(LevelCreator levelCreator, GameWatcher gameWatcher)
    {
        _levelCreator = levelCreator;
        _gameWatcher = gameWatcher;
    }

    protected override void Init()
    {
        _gameWatcher.ThereWasADefeat += Open;
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _restartButton.onClick.AddListener(OnExitButtonClick);
    }

    protected override void OnOpen()
    {
        _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
    }

    private void OnRestartButtonClick()
    {
        _levelCreator.RestartLevel();
        Close();
    }

    private void OnExitButtonClick()
    {
        //TODO подумать как можно выйти в главное меню не перезагружая сцену
    }
}
