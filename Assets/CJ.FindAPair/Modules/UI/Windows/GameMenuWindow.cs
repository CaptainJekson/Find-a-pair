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
    private AudioController _audioController;

    [Inject]
    public void Construct(LevelCreator levelCreator, ISaver gameSaver, AudioController audioController)
    {
        _levelCreator = levelCreator;
        _gameSaver = gameSaver;
        _audioController = audioController;
    }
    
    protected override void OnOpen()
    {
        Time.timeScale = 0.0f;
        _audioController.PlaySound(_audioController.AudioClipsCollection.WindowOpenSound);
        _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
        ChangeStateRestartNoEnergyButton();
    }

    protected override void OnClose()
    {
        Time.timeScale = 1.0f;
    }
    
    protected override void OnCloseButtonClick()
    {
        _audioController.PlaySound(_audioController.AudioClipsCollection.WindowCloseSound);
    }

    private void ChangeStateRestartNoEnergyButton()
    {
        var saveData = _gameSaver.LoadData();
        _restartNoEnergyButton.gameObject.SetActive(saveData.ItemsData.Energy <= 1);
    }
}