using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Tutorial;
using CJ.FindAPair.Modules.UI.Windows;
using DG.Tweening;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes
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
            BlockGameWithDelay();

            CheckFirstTutorial();
        }

        private void BlockGameWithDelay()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(6.7f);
            sequence.AppendCallback(() => _uiRoot.OpenWindow<FullBlockerWindow>());
        }
        
        private void CheckFirstTutorial()
        {
            if (_levelCreator.LevelConfig.LevelNumber == 1)
            {
                _tutorialRoot.ShowTutorial<FirstTutorialScreen>(2.0f);
                _tutorialRoot.SetActionForStep<FirstTutorialScreen>(ActionAfterClickOneCard,1);

                var screen = _tutorialRoot.GetScreen<FirstTutorialScreen>();
                var pairCards = FindPairCards();
                var sequence = DOTween.Sequence();
                sequence.AppendInterval(6.8f);
                sequence.AppendCallback(() =>
                {
                    pairCards[0].EnableInteractable();
                    screen.SetPositionTapForOneCard(pairCards[0].transform.position);
                });
            }

            void ActionAfterClickOneCard()
            {
                _uiRoot.CloseWindow<FullBlockerWindow>();
            }

            List<Card> FindPairCards()
            {
                var pairCards = new List<Card>();
                var oneCard = _levelCreator.Cards[0];
                pairCards.Add(oneCard);

                for (var i = 1; i < _levelCreator.Cards.Count; i++)
                {
                    if (oneCard.NumberPair == _levelCreator.Cards[i].NumberPair)
                    {
                        pairCards.Add(_levelCreator.Cards[i]);
                    }
                }
                
                return pairCards;
            }
        }
    }
}
