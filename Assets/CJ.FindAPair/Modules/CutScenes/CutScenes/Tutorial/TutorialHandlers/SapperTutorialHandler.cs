using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Booster;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Tutorial;
using CJ.FindAPair.Modules.UI.Windows;
using DG.Tweening;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes.Tutorial.TutorialHandlers
{
    public class SapperTutorialHandler : TutorialHandler
    {
        private readonly UIRoot _uiRoot;
        private readonly BoosterHandler _boosterHandler;

        private SapperBoosterTutorialScreen _tutorialScreen;
        private BoosterInterfaceWindow _boosterInterfaceWindow;
        
        public SapperTutorialHandler(LevelCreator levelCreator, BoosterHandler boosterHandler,
            TutorialRoot tutorialRoot, UIRoot uiRoot) : base(levelCreator, tutorialRoot)
        {
            _uiRoot = uiRoot;
            _boosterHandler = boosterHandler;
            _boosterInterfaceWindow = _uiRoot.GetWindow<BoosterInterfaceWindow>();
        }

        public override void Activate()
        {
            AllDisableCard();

            _tutorialScreen = _tutorialRoot.GetScreen<SapperBoosterTutorialScreen>();
            _tutorialRoot.ShowTutorial<SapperBoosterTutorialScreen>(7.0f);
            _tutorialRoot.SetActionForStep<SapperBoosterTutorialScreen>(
                () => _boosterHandler.BoosterActivationHandler(BoosterType.Sapper), 0);
            _tutorialRoot.SetActionForStep<SapperBoosterTutorialScreen>(AllEnableCard, 1);

            var sequence = DOTween.Sequence();
            sequence.AppendInterval(2.0f);
            sequence.AppendCallback(() =>
            {
                _tutorialScreen.SetPositionTapForSapperBooster(_boosterInterfaceWindow.SapperTransform.position);
            });
        }
    }
}