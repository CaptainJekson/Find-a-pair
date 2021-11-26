using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;
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
    private UIRoot _uiRoot;
    private ISaver _gameSaver;

    [Inject]
    public void Construct(LevelCreator levelCreator, EnergyCooldownHandler energyCooldownHandler, UIRoot uiRoot, ISaver gameSaver)
    {
        _levelCreator = levelCreator;
        _energyCooldownHandler = energyCooldownHandler;
        _uiRoot = uiRoot;
        _gameSaver = gameSaver;
    }

    protected override void OnOpen()
    {
        _exitButton.onClick.AddListener(_energyCooldownHandler.TryDecreaseScore);
        _restartButton.onClick.AddListener(OnRestartButtonClick);

        Time.timeScale = 0.0f;
        _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
    }

    protected override void OnClose()
    {
        _exitButton.onClick.RemoveListener(_energyCooldownHandler.TryDecreaseScore);
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);

        Time.timeScale = 1.0f;
    }
    
    private void OnRestartButtonClick()
    {
        var saveData = _gameSaver.LoadData();
            
        if (saveData.ItemsData.Energy <= 0)
        {
            _uiRoot.OpenWindow<EnergyBoostOfferWindow>();  
        }
        else
        {
            _uiRoot.OpenWindow<ConfirmRestartWindow>();
            _uiRoot.CloseWindow<GameBlockWindow>();
            Close();
        }
    }
}