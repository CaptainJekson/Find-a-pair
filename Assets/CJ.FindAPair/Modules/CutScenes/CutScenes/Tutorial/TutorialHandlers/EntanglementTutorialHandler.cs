using System.Linq;
using CJ.FindAPair.Constants;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Tutorial;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes.Tutorial.TutorialHandlers
{
    public class EntanglementTutorialHandler : TutorialHandler
    {
        private readonly LevelCreator _levelCreator;
        private readonly CardsPlacer _cardsPlacer;

        private EntanglementCardTutorialScreen _tutorialScreen;
        private Card _fortuneCard;
        
        public EntanglementTutorialHandler( LevelCreator levelCreator, TutorialRoot tutorialRoot, CardsPlacer cardsPlacer) 
            : base(levelCreator, tutorialRoot)
        {
            _levelCreator = levelCreator;
            _cardsPlacer = cardsPlacer;
        }

        public override void Activate()
        {
            AllDisableCard();
                
            _tutorialScreen = _tutorialRoot.GetScreen<EntanglementCardTutorialScreen>();
            _fortuneCard = GetFortuneCard();
            _tutorialRoot.SetActionForStep<EntanglementCardTutorialScreen>(AllEnableCard,1);

            _cardsPlacer.CardsDealt += OnCardDealt;
        }

        private Card GetFortuneCard()
        {
            return _levelCreator.Cards.FirstOrDefault(card => card.NumberPair == ConstantsCard.NUMBER_ENTANGLEMENT);
        }
        
        private void OnCardDealt()
        {
            _cardsPlacer.CardsDealt -= OnCardDealt;
            _tutorialScreen.SetPositionTapForEntanglementCard(_fortuneCard.transform.position);
            _tutorialRoot.ShowTutorial<EntanglementCardTutorialScreen>();
            _fortuneCard.EnableForTutorial();
        }
    }
}