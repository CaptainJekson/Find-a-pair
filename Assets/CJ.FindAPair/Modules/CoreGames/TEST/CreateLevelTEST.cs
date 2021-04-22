using CJ.FindAPair.Modules.CoreGames.Configs;

namespace CJ.FindAPair.Modules.CoreGames.TEST
{
    public class CreateLevelTEST
    {
        private LevelCreator _levelCreator;
        private LevelConfigCollection _levelConfigCollection;
        
        public CreateLevelTEST(LevelCreator levelCreator, LevelConfigCollection levelConfigCollection)
        {
            _levelCreator = levelCreator;
            _levelConfigCollection = levelConfigCollection;
            _levelCreator.CreateLevel(_levelConfigCollection.Levels[0]);
        }
    }
}