using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Booster;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Tutorial;
using CJ.FindAPair.Modules.UI.Windows;
using DG.Tweening;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes.Tutorial.TutorialHandlers
{
    public class MagnetTutorialHandler : TutorialHandler
    {
        private readonly UIRoot _uiRoot;
        private readonly BoosterHandler _boosterHandler;

        private MagnetBoosterTutorialScreen _tutorialScreen;
        private BoosterInterfaceWindow _boosterInterfaceWindow;
        
        public MagnetTutorialHandler(LevelCreator levelCreator, BoosterHandler boosterHandler,
            TutorialRoot tutorialRoot, UIRoot uiRoot) : base(levelCreator, tutorialRoot)
        {
            _uiRoot = uiRoot;
            _boosterHandler = boosterHandler;
            _boosterInterfaceWindow = _uiRoot.GetWindow<BoosterInterfaceWindow>();
        }

        public override void Activate()
        {
            AllDisableCard();

            _tutorialScreen = _tutorialRoot.GetScreen<MagnetBoosterTutorialScreen>();
            _tutorialRoot.ShowTutorial<MagnetBoosterTutorialScreen>(7.0f);
            _tutorialRoot.SetActionForStep<MagnetBoosterTutorialScreen>(
                () => _boosterHandler.BoosterActivationHandler(BoosterType.Magnet), 0);
            _tutorialRoot.SetActionForStep<MagnetBoosterTutorialScreen>(AllEnableCard, 1);

            var sequence = DOTween.Sequence();
            sequence.AppendInterval(2.0f);
            sequence.AppendCallback(() =>
            {
                _tutorialScreen.SetPositionTapForDetectorBooster(_boosterInterfaceWindow.MagnetTransform.position);
            });
        }
    }
}