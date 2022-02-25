using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Booster;
using CJ.FindAPair.Modules.CoreGames.SpecialCards;
using CJ.FindAPair.Modules.CutScenes.CutScenes.Tutorial.TutorialHandlers;
using CJ.FindAPair.Modules.UI.Installer;
using UnityEngine;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes.Tutorial
{
    public class TutorialDriver
    {
        private readonly TutorialRoot _tutorialRoot;
        private readonly UIRoot _uiRoot;
        private readonly LevelCreator _levelCreator;
        private readonly CardsPlacer _cardsPlacer;
        private readonly BoosterHandler _boosterHandler;
        private readonly SpecialCardHandler _specialCardHandler;
        private readonly NextLevelCutScene _nextLevelCutScene;
        private readonly ISaver _gameSaver;
        
        private FirstTutorialHandler _firstTutorialHandler;
        private DetectorTutorialHandler _detectorTutorialHandler;
        private MagnetTutorialHandler _magnetTutorialHandler;
        private FortuneTutorialHandler _fortuneTutorialHandler;
        private SapperTutorialHandler _sapperTutorialHandler;
        private EntanglementTutorialHandler _entanglementTutorialHandler;
        private HardLevelTutorialHandler _hardLevelTutorialHandler;

        public TutorialDriver(TutorialRoot tutorialRoot, UIRoot uiRoot, LevelCreator levelCreator, 
            CardsPlacer cardsPlacer, BoosterHandler boosterHandler, SpecialCardHandler specialCardHandler,
            NextLevelCutScene nextLevelCutScene, ISaver gameSaver)
        {
            _tutorialRoot = tutorialRoot;
            _uiRoot = uiRoot;
            _levelCreator = levelCreator;
            _cardsPlacer = cardsPlacer;
            _nextLevelCutScene = nextLevelCutScene;
            _gameSaver = gameSaver;
            _boosterHandler = boosterHandler;
            _specialCardHandler = specialCardHandler;
            _levelCreator.LevelCreated += CheckTutorialLevels;
            _nextLevelCutScene.MoveMarkerComplete += CheckTutorialAfterNextLevelCutScene;
            
            CreateTutorialHandlers();
        }

        private void CreateTutorialHandlers()
        {
            _firstTutorialHandler = new FirstTutorialHandler(_levelCreator, _tutorialRoot, _cardsPlacer, _uiRoot);
            _detectorTutorialHandler = new DetectorTutorialHandler(_levelCreator, _boosterHandler,_tutorialRoot, _uiRoot);
            _magnetTutorialHandler = new MagnetTutorialHandler(_levelCreator, _boosterHandler,_tutorialRoot, _uiRoot);
            _fortuneTutorialHandler = new FortuneTutorialHandler(_levelCreator, _tutorialRoot, _cardsPlacer,
                _specialCardHandler);
            _sapperTutorialHandler = new SapperTutorialHandler(_levelCreator, _boosterHandler,_tutorialRoot, _uiRoot);
            _entanglementTutorialHandler = new EntanglementTutorialHandler(_levelCreator, _tutorialRoot, _cardsPlacer);
            _hardLevelTutorialHandler = new HardLevelTutorialHandler(_levelCreator, _tutorialRoot);
        }

        private void CheckTutorialLevels()
        {
            if (_gameSaver.LoadData().CurrentLevel <= 1)
            {
                _firstTutorialHandler.Activate();
            }

            if (_gameSaver.LoadData().CurrentLevel == 10)
            {
                _detectorTutorialHandler.Activate();
            }
            
            if (_gameSaver.LoadData().CurrentLevel == 28)
            {
                _magnetTutorialHandler.Activate();
            }

            if (_gameSaver.LoadData().CurrentLevel == 34)
            {
                _fortuneTutorialHandler.Activate();
            }

            if (_gameSaver.LoadData().CurrentLevel == 42)
            {
                _sapperTutorialHandler.Activate();
            }
            
            if (_gameSaver.LoadData().CurrentLevel == 97)
            {
                _entanglementTutorialHandler.Activate();
            }
        }

        private void CheckTutorialAfterNextLevelCutScene()
        {
            if (_gameSaver.LoadData().CurrentLevel == 14)
            {
                _nextLevelCutScene.NotOpenPreviewWindowNextTime();
                _hardLevelTutorialHandler.Activate();
            }
        }
    }
}
