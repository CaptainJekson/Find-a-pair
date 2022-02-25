using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.UI.Installer;
using UnityEngine;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes.Tutorial.TutorialHandlers
{
    public abstract class TutorialHandler
    {
        private LevelCreator _levelCreator;
        protected TutorialRoot _tutorialRoot;

        protected TutorialHandler(LevelCreator levelCreator, TutorialRoot tutorialRoot)
        {
            _levelCreator = levelCreator;
            _tutorialRoot = tutorialRoot;
        }
    
        public abstract void Activate();

        protected void AllDisableCard()
        {
            foreach (var card in _levelCreator.Cards)
            {
                card.DisableForTutorial();
            }
        }

        protected void AllEnableCard()
        {
            foreach (var card in _levelCreator.Cards)
            {
                card.EnableForTutorial();
            }
        }
    }
}
