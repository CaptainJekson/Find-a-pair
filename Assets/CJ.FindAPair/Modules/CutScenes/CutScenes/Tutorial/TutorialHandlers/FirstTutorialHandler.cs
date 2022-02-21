using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Tutorial;
using CJ.FindAPair.Modules.UI.Windows;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes.Tutorial.TutorialHandlers
{
    public class FirstTutorialHandler : TutorialHandler
    {
        private readonly TutorialRoot _tutorialRoot;
        private readonly LevelCreator _levelCreator;
        private readonly CardsPlacer _cardsPlacer;

        private FirstTutorialScreen _tutorialScreen;
        private List<Card> _cardsPair;
        private GameInterfaceWindow _gameInterfaceWindow;

        public FirstTutorialHandler( LevelCreator levelCreator, TutorialRoot tutorialRoot, CardsPlacer cardsPlacer,
            UIRoot gameInterfaceWindow) : base(levelCreator)
        {
            _tutorialRoot = tutorialRoot;
            _levelCreator = levelCreator;
            _cardsPlacer = cardsPlacer;
            _gameInterfaceWindow = gameInterfaceWindow.GetWindow<GameInterfaceWindow>();
        }
        
        public override void Activate()
        {
            AllDisableCard();
            _tutorialScreen = _tutorialRoot.GetScreen<FirstTutorialScreen>();
            _cardsPair = GetFindPairCards();

            _tutorialRoot.ShowTutorial<FirstTutorialScreen>(2.0f);
            _tutorialRoot.SetActionForStep<FirstTutorialScreen>(
                () => _cardsPair[0].EnableForTutorial(),0);
            _tutorialRoot.SetActionForStep<FirstTutorialScreen>(
                () => _cardsPair[1].EnableForTutorial(),1);
            _tutorialRoot.SetActionForStep<FirstTutorialScreen>(AllEnableCard,5);

            _cardsPlacer.CardsDealt += OnCardDealt;
        }

        private List<Card> GetFindPairCards()
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

        private void OnCardDealt()
        {
            _cardsPlacer.CardsDealt -= OnCardDealt;
            _tutorialScreen.SetPositionTapForOneCard(_cardsPair[0].transform.position);
            _tutorialScreen.SetPositionTapForTwoCard(_cardsPair[1].transform.position);
            _tutorialScreen.SetPositionPointerOnCoins(_gameInterfaceWindow.ScorePanelForTutorial.position);
            _tutorialScreen.SetPositionPointerOnTimer(_gameInterfaceWindow.TimePanelForTutorial.position);
            _tutorialScreen.SetPositionPointerOnLives(_gameInterfaceWindow.LifePanelForTutorial.position);
        }
    }
}