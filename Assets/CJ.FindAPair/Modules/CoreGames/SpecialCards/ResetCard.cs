using System.Linq;
using CJ.FindAPair.Constants;

namespace CJ.FindAPair.Modules.CoreGames.SpecialCards
{
    public class ResetCard : SpecialCard
    {
        public override void OpenSpecialCard(Card specialCardOld)
        {
            foreach (var card in _levelCreator.Cards.Where(card => card.NumberPair < ConstantsCard.NUMBER_SPECIAL))
                card.Hide();

            _gameWatcher.ResetScore();
        }
    }
}