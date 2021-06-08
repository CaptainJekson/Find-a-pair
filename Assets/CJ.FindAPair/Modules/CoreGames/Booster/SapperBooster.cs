using System.Linq;
using CJ.FindAPair.Animation;
using CJ.FindAPair.Constants;

namespace CJ.FindAPair.Modules.CoreGames.Booster
{
    public class SapperBooster : Modules.CoreGames.Booster.Booster
    {
        public override void ActivateBooster()
        {
            foreach (var card in _levelCreator.Cards
                .Where(card => card.NumberPair >= ConstantsCard.NUMBER_SPECIAL))
            {
                card.Show(true);
                card.IsMatched = true;
                card.GetComponent<AnimationCardOld>().PlaySapper();
            }
        }
    }
}