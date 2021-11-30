using System.Linq;
using CJ.FindAPair.Constants;
using UnityEngine;
using UnityEngine.Events;

namespace CJ.FindAPair.Modules.CoreGames.SpecialCards
{
    public class FortuneCard : SpecialCard
    {
        public event UnityAction CardRealised;
        
        public override void OpenSpecialCard(Card specialCard)
        {
            var randomChance = Random.Range(0, 2);
            var randomCard = _levelCreator.Cards[Random.Range(0, _levelCreator.Cards.Count)];

            if (randomChance > 0)
                OpeningPairCards(randomCard);
            else
                ClosingPairCards(randomCard);

            specialCard.DelayHide();
        }
        
        private void OpeningPairCards(Card randomCardOld)
        {
            randomCardOld = GetRandomCard(randomCardOld, true);

            foreach (var card in _levelCreator.Cards.Where(card => card.NumberPair == randomCardOld.NumberPair))
                card.Show();
        }

        private void ClosingPairCards(Card randomCardOld)
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
                CardRealised?.Invoke();
            }
        }
        
        private Card GetRandomCard(Card randomCardOld, bool isMatched) //TODO Repeting MagicEyeBooster
        {
            while (!(isMatched ^ randomCardOld.IsMatched) || randomCardOld.NumberPair >= ConstantsCard.NUMBER_SPECIAL)
            {
                randomCardOld = _levelCreator.Cards[Random.Range(0, _levelCreator.Cards.Count)];
            }

            return randomCardOld;
        }
    }
}