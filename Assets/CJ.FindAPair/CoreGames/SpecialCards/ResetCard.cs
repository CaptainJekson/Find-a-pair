using System.Linq;
using CJ.FindAPair.CardTable;
using CJ.FindAPair.Constants;
using CJ.FindAPair.CoreGames;

namespace CJ.FindAPair.Game.SpecialCards
{
    public class ResetCard : SpecialCard
    {
        public override void OpenSpecialCard(Card specialCard)
        {
            foreach (var card in _levelCreator.Cards.Where(card => card.NumberPair < ConstantsCard.NUMBER_SPECIAL))
                card.Hide();
            
            _gameWatcher.ResetScore();
            specialCard.DelayHide();
        }
    }
}