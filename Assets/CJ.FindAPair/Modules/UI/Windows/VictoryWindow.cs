using CJ.FindAPair.Modules.CoreGames;
using Zenject;

public class VictoryWindow : Window //TODO реализовать кнопки рестарт выход и след лвл
{
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
        _gameWatcher.ThereWasAVictory += Open;
    }
}
