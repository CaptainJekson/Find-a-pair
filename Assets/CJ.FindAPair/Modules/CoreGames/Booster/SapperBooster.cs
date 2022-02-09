using System.Linq;
using CJ.FindAPair.Constants;

namespace CJ.FindAPair.Modules.CoreGames.Booster
{
    public class SapperBooster : Booster
    {
        public override void ActivateBooster()
        {
            _audioController.PlaySound(_audioController.AudioClipsCollection.SapperBoosterSound);
            
            foreach (var card in _levelCreator.Cards
                .Where(card => card.NumberPair >= ConstantsCard.NUMBER_SPECIAL))
            {
                card.Show(true);
                card.SetMatchedState();
                card.PlayDestroySpecialCard();
            }
        }
    }
}