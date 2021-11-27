using CJ.FindAPair.Modules.CoreGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameMenuWindow : Window
{
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    [SerializeField] private Button _exitButton;

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
        _exitButton.onClick.AddListener(_energyCooldownHandler.DecreaseScore);

        Time.timeScale = 0.0f;
        _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
    }

    protected override void OnClose()
    {
        _exitButton.onClick.RemoveListener(_energyCooldownHandler.DecreaseScore);

        Time.timeScale = 1.0f;
    }
}