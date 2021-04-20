using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using UnityEngine;

namespace CJ.FindAPair.CoreGames.TEST
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