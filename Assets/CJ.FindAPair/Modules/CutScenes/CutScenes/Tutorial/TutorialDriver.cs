using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Booster;
using CJ.FindAPair.Modules.CutScenes.CutScenes.Tutorial.TutorialHandlers;
using CJ.FindAPair.Modules.UI.Installer;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes.Tutorial
{
    public class TutorialDriver
    {
        private readonly TutorialRoot _tutorialRoot;
        private readonly UIRoot _uiRoot;
        private readonly LevelCreator _levelCreator;
        private readonly CardsPlacer _cardsPlacer;
        private readonly BoosterHandler _boosterHandler;
        private readonly ISaver _gameSaver;
        
        private FirstTutorialHandler _firstTutorialHandler;
        private DetectorTutorialHandler _detectorTutorialHandler;

        public TutorialDriver(TutorialRoot tutorialRoot, UIRoot uiRoot, LevelCreator levelCreator, 
            CardsPlacer cardsPlacer, BoosterHandler boosterHandler, ISaver gameSaver)
        {
            _tutorialRoot = tutorialRoot;
            _uiRoot = uiRoot;
            _levelCreator = levelCreator;
            _cardsPlacer = cardsPlacer;
            _gameSaver = gameSaver;
            _boosterHandler = boosterHandler;
            _levelCreator.LevelCreated += CheckTutorialLevels;
            
            CreateTutorialHandlers();
        }

        private void CreateTutorialHandlers()
        {
            _firstTutorialHandler = new FirstTutorialHandler(_levelCreator, _tutorialRoot, _cardsPlacer, _uiRoot);
            _detectorTutorialHandler = new DetectorTutorialHandler(_levelCreator, _boosterHandler,
                _tutorialRoot, _uiRoot);
        }

        private void CheckTutorialLevels()
        {
            if (_gameSaver.LoadData().CurrentLevel <= 1)
            {
                _firstTutorialHandler.Activate();
            }

            if (_levelCreator.LevelConfig.LevelNumber == 10) //if (_gameSaver.LoadData().CurrentLevel == 10)
            {
                _detectorTutorialHandler.Activate();
            }
        }
    }
}
