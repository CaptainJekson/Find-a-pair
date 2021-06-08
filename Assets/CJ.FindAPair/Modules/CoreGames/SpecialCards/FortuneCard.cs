using System.Linq;
using CJ.FindAPair.Constants;
using UnityEngine;

namespace CJ.FindAPair.Modules.CoreGames.SpecialCards
{
    public class FortuneCard : SpecialCard
    {
        public override void OpenSpecialCard(CardOld specialCardOld)
        {
            var randomChance = Random.Range(0, 2);
            var randomCard = _levelCreator.Cards[Random.Range(0, _levelCreator.Cards.Count)];

            if (randomChance > 0)
                OpeningPairCards(randomCard);
            else
                ClosingPairCards(randomCard);

            specialCardOld.DelayHide();
        }
        
        private void OpeningPairCards(CardOld randomCardOld)
        {
            randomCardOld = GetRandomCard(randomCardOld, true);

            foreach (var card in _levelCreator.Cards.Where(card => card.NumberPair == randomCardOld.NumberPair))
                card.Show();
        }

        private void ClosingPairCards(CardOld randomCardOld)
        {
            var quantityMatchedCards = _levelCreator.Cards.Count(card => card.IsMatched);
            _gameWatcher.RemoveQuantityOfMatchedPairs();

            if (quantityMatchedCards > 0)
            {
                randomCardOld = GetRandomCard(randomCardOld, false);

                foreach (var card in _levelCreator.Cards.Where(card => card.NumberPair == randomCardOld.NumberPair))
                    card.Hide();
            }
            else
            {
                _gameWatcher.InitiateDefeat();
            }
        }
        
        private CardOld GetRandomCard(CardOld randomCardOld, bool isMatched) //TODO Repeting MagicEyeBooster
        {
            while (!(isMatched ^ randomCardOld.IsMatched) || randomCardOld.NumberPair >= ConstantsCard.NUMBER_SPECIAL)
            {
                randomCardOld = _levelCreator.Cards[Random.Range(0, _levelCreator.Cards.Count)];
            }

            return randomCardOld;
        }
    }
}