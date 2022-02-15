using System.Linq;
using CJ.FindAPair.Constants;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.SpecialCards;
using CJ.FindAPair.Modules.CutScenes.CutScenes.Tutorial.TutorialHandlers;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Tutorial;
using UnityEngine;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes.Tutorial
{
    public class FortuneTutorialHandler : TutorialHandler
    {
        private readonly TutorialRoot _tutorialRoot;
        private readonly LevelCreator _levelCreator;
        private readonly CardsPlacer _cardsPlacer;
        private readonly SpecialCardHandler _specialCardHandler;

        private FortuneCardTutorialScreen _tutorialScreen;
        private Card _fortuneCard;
        
        public FortuneTutorialHandler( LevelCreator levelCreator, TutorialRoot tutorialRoot, CardsPlacer cardsPlacer,
            SpecialCardHandler specialCardHandler) 
            : base(levelCreator)
        {
            _levelCreator = levelCreator;
            _tutorialRoot = tutorialRoot;
            _cardsPlacer = cardsPlacer;
            _specialCardHandler = specialCardHandler;
        }

        public override void Activate()
        {
            AllDisableCard();
                
            _tutorialScreen = _tutorialRoot.GetScreen<FortuneCardTutorialScreen>();
            _fortuneCard = GetFortuneCard();
            _tutorialRoot.SetActionForStep<FortuneCardTutorialScreen>(AllEnableCard,1);

            _cardsPlacer.CardsDealt += OnCardDealt;
        }

        private Card GetFortuneCard()
        {
            return _levelCreator.Cards.FirstOrDefault(card => card.NumberPair == ConstantsCard.NUMBER_FORTUNE);
        }
        
        private void OnCardDealt()
        {
            _cardsPlacer.CardsDealt -= OnCardDealt;
            _tutorialScreen.SetPositionTapForFortuneCard(_fortuneCard.transform.position);
            _tutorialRoot.ShowTutorial<FortuneCardTutorialScreen>();
            _specialCardHandler.GetFortuneCardHandler().MakeOneHundredPercentOpening();
            _fortuneCard.EnableForTutorial();
        }
    }
}