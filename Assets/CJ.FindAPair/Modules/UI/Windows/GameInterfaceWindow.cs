using System;
using CJ.FindAPair.Modules.CoreGames;
using TMPro;
using UnityEngine;
using Zenject;

public class GameInterfaceWindow : Window
{
    [SerializeField] private TextMeshProUGUI _lifeValueText;
    [SerializeField] private TextMeshProUGUI _timeValueText;
    [SerializeField] private TextMeshProUGUI _scoreValueText;
    [SerializeField] private RectTransform _coinTransferPoint;

    private GameWatcher _gameWatcher;
    private LevelCreator _levelCreator;

    [Inject]
    public void Construct(GameWatcher gameWatcher, LevelCreator levelCreator)
    {
        _levelCreator = levelCreator;
        _gameWatcher = gameWatcher;
    }

    protected override void OnOpen()
    {
        _gameWatcher.LifeСhanged += SetLifeValue;
        _gameWatcher.ScoreСhanged += SetScoreValue;
        _gameWatcher.TimeСhanged += SetTimeValue;
        SetData();
    }

    protected override void OnClose()
    {
        _gameWatcher.LifeСhanged -= SetLifeValue;
        _gameWatcher.ScoreСhanged -= SetScoreValue;
        _gameWatcher.TimeСhanged -= SetTimeValue;
    }
    
    private void SetData()
    {
        SetLifeValue(_levelCreator.LevelConfig.Tries);
        SetScoreValue(0);
        SetTimeValue(_levelCreator.LevelConfig.Time);
    }
    
    private void SetLifeValue(int value)
    {
        _lifeValueText.SetText(value.ToString());
    }
    
    private void SetScoreValue(int value)
    {
        _scoreValueText.SetText(value.ToString());
    }
    
    private void SetTimeValue(int value)
    {
        _timeValueText.SetText(TimeConverter(value));
    }

    private string TimeConverter(int secondTime)
    {
        var time = TimeSpan.FromSeconds(secondTime);

        return time.ToString(@"mm\:ss");
    }

    public Vector3 GetCoinPosition()
    {
        return _coinTransferPoint.position;
    }
}
