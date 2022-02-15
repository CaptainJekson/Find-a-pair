using CJ.FindAPair.Modules.CoreGames;
using UnityEngine;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes.Tutorial.TutorialHandlers
{
    public abstract class TutorialHandler
    {
        private LevelCreator _levelCreator;

        protected TutorialHandler(LevelCreator levelCreator)
        {
            _levelCreator = levelCreator;
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
