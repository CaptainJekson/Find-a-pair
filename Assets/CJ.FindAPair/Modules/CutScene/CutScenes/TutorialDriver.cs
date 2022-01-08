using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Tutorial;
using UnityEngine;

namespace CJ.FindAPair.Modules.CutScene.CutScenes
{
    public class TutorialDriver
    {
        private readonly TutorialRoot _tutorialRoot;
        private readonly LevelCreator _levelCreator;
        
        public TutorialDriver(TutorialRoot tutorialRoot, LevelCreator levelCreator)
        {
            _tutorialRoot = tutorialRoot;
            _levelCreator = levelCreator;
            _levelCreator.LevelCreated += CheckTutorialLevels;
        }

        private void CheckTutorialLevels()
        {
            if (_levelCreator.LevelConfig.LevelNumber == 1)
            {
                _tutorialRoot.ShowTutorial<FirstTutorialScreen>(2.0f);
            }
        }
    }
}
