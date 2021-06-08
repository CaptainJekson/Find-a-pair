using System.Linq;
using CJ.FindAPair.Animation;
using CJ.FindAPair.Constants;
using UnityEngine;

namespace CJ.FindAPair.Modules.CoreGames.Booster
{
    public class MagicEyeBooster : Modules.CoreGames.Booster.Booster
    {
        public override void ActivateBooster()
        {
            var randomCard = _levelCreator.Cards[Random.Range(0, _levelCreator.Cards.Count)];

            randomCard = GetRandomCard(randomCard, true);

            foreach (var card in _levelCreator.Cards.Where(card => card.NumberPair == randomCard.NumberPair))
                card.GetComponent<AnimationCardOld>().PlayMagicEye();
        }
        
        private CardOld GetRandomCard(CardOld randomCardOld, bool isMatched)
        {
            while (!(isMatched ^ randomCardOld.IsMatched) || randomCardOld.NumberPair >= ConstantsCard.NUMBER_SPECIAL)
            {
                randomCardOld = _levelCreator.Cards[Random.Range(0, _levelCreator.Cards.Count)];
            }

            return randomCardOld;
        }
    }
}