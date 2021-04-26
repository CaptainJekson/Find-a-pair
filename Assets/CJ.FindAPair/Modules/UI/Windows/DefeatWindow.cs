using CJ.FindAPair.Modules.CoreGames;
using Zenject;

public class DefeatWindow : Window //TODO реализовать кнопки рестарт выход
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
        _gameWatcher.ThereWasADefeat += Open;
    }
}
