using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Tutorial;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes.Tutorial.TutorialHandlers
{
    public class HardLevelTutorialHandler : TutorialHandler
    {
        public HardLevelTutorialHandler(LevelCreator levelCreator, TutorialRoot tutorialRoot) : base(levelCreator,
            tutorialRoot)
        {
        }

        public override void Activate()
        {
            _tutorialRoot.ShowTutorial<HardLevelTutorialScreen>();
        }
    }
}