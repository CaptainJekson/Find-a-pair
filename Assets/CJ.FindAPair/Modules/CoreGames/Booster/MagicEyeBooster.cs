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
                card.GetComponent<AnimationCard>().PlayMagicEye();
        }
        
        private Card GetRandomCard(Card randomCard, bool isMatched)
        {
            while (!(isMatched ^ randomCard.IsMatched) || randomCard.NumberPair >= ConstantsCard.NUMBER_SPECIAL)
            {
                randomCard = _levelCreator.Cards[Random.Range(0, _levelCreator.Cards.Count)];
            }

            return randomCard;
        }
    }
}