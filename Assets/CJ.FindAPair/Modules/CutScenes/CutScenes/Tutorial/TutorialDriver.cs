using CJ.FindAPair.Modules.CoreGames;
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

        private FirstTutorialHandler _firstTutorialHandler;

        public TutorialDriver(TutorialRoot tutorialRoot, UIRoot uiRoot, LevelCreator levelCreator, 
            CardsPlacer cardsPlacer)
        {
            _tutorialRoot = tutorialRoot;
            _uiRoot = uiRoot;
            _levelCreator = levelCreator;
            _cardsPlacer = cardsPlacer;
            _levelCreator.LevelCreated += CheckTutorialLevels;
            
            CreateTutorialHandlers();
        }

        private void CreateTutorialHandlers()
        {
            _firstTutorialHandler = new FirstTutorialHandler(_tutorialRoot, _uiRoot, _levelCreator, _cardsPlacer);
        }

        private void CheckTutorialLevels()
        {
            _firstTutorialHandler.CheckFirstTutorial();
        }
    }
}
