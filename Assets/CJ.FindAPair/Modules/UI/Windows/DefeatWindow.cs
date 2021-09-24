using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.SpecialCards;
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
    [SerializeField] private TextMeshProUGUI _defeatNotificationText;
    [SerializeField] private string _timeIsOverMessage;
    [SerializeField] private string _livesAreOverMessage;
    [SerializeField] private string _bombDetonatedMessage;
    [SerializeField] private string _fortuneCardRealisedMessage;

    private LevelCreator _levelCreator;
    private GameWatcher _gameWatcher;
    private BombCard _bombCard;
    private FortuneCard _fortuneCard;
    private string _currentMessage;

    [Inject]
    public void Construct(LevelCreator levelCreator, GameWatcher gameWatcher,
        BombCard bombCard, FortuneCard fortuneCard)
    {
        _levelCreator = levelCreator;
        _gameWatcher = gameWatcher;
        _bombCard = bombCard;
        _fortuneCard = fortuneCard;
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
        _gameWatcher.TimeIsOut += TimeIsOver;
        _gameWatcher.LivesIsOut += LivesAreOver;
        _bombCard.BombDetonate += BombDetonated;
        _fortuneCard.CardRealised += FortuneCardRealised;

        _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
        _defeatNotificationText.SetText(_currentMessage);
    }

    protected override void OnClose()
    {
        _gameWatcher.TimeIsOut -= TimeIsOver;
        _gameWatcher.LivesIsOut -= LivesAreOver;
        _bombCard.BombDetonate -= BombDetonated;
        _fortuneCard.CardRealised -= FortuneCardRealised;
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

    private void TimeIsOver()
    {
        _currentMessage = _timeIsOverMessage;
    }

    private void LivesAreOver()
    {
        _currentMessage = _livesAreOverMessage;
    }

    private void BombDetonated()
    {
        _currentMessage = _bombDetonatedMessage;
    }

    private void FortuneCardRealised()
    {
        _currentMessage = _fortuneCardRealisedMessage;
    }
}