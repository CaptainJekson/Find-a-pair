using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Tutorial;
using CJ.FindAPair.Modules.UI.Windows;
using DG.Tweening;

namespace CJ.FindAPair.Modules.CutScene.CutScenes
{
    public class TutorialDriver
    {
        private readonly TutorialRoot _tutorialRoot;
        private readonly UIRoot _uiRoot;
        private readonly LevelCreator _levelCreator;
        
        public TutorialDriver(TutorialRoot tutorialRoot, UIRoot uiRoot, LevelCreator levelCreator)
        {
            _tutorialRoot = tutorialRoot;
            _uiRoot = uiRoot;
            _levelCreator = levelCreator;
            _levelCreator.LevelCreated += CheckTutorialLevels;
        }

        private void CheckTutorialLevels()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(6.7f);
            sequence.AppendCallback(() => _uiRoot.OpenWindow<FullBlockerWindow>());
            
            if (_levelCreator.LevelConfig.LevelNumber == 1)
            {
                _tutorialRoot.ShowTutorial<FirstTutorialScreen>(2.0f);
                _tutorialRoot.SetActionForStep<FirstTutorialScreen>(() => _uiRoot.CloseWindow<FullBlockerWindow>(), 
                    1);
            }
        }
    }
}
