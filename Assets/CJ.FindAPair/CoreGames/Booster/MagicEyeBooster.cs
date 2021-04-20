using System.Linq;
using CJ.FindAPair.Animation;
using CJ.FindAPair.CardTable;
using CJ.FindAPair.Constants;
using CJ.FindAPair.CoreGames;
using UnityEngine;

namespace CJ.FindAPair.Game.Booster
{
    public class MagicEyeBooster : Booster
    {
        public override void ActivateBooster()
        {
            var randomCard = _levelCreator.Cards[Random.Range(0, _levelCreator.Cards.Count)];

            randomCard = GetRandomCard(randomCard, true);

            foreach (var card in _levelCreator.Cards.Where(card => card.NumberPair == randomCard.NumberPair))
                card.GetComponent<AnimationCard>().PlayMagicEye();
        }
        
        private Card GetRandomCard(Card randomCard, bool isMatched) //TODO Repeting FortuneCard
        {
            while (!(isMatched ^ randomCard.IsMatched) || randomCard.NumberPair >= ConstantsCard.NUMBER_SPECIAL)
            {
                randomCard = _levelCreator.Cards[Random.Range(0, _levelCreator.Cards.Count)];
            }

            return randomCard;
        }
    }
}