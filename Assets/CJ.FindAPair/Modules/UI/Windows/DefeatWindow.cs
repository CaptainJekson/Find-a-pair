using CJ.FindAPair.Modules.CoreGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DefeatWindow : Window
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _adsButton;
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
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _adsButton.onClick.AddListener(OnAdsButtonClick);
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
        _levelCreator.ClearLevel();
        Close();
    }

    private void OnAdsButtonClick()
    {
    }
}