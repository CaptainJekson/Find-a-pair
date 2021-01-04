﻿using System.Linq;
using CJ.FindAPair.CardTable;
using CJ.FindAPair.Constants;
using UnityEngine;

namespace CJ.FindAPair.Game.SpecialCards
{
    public class FortuneCard : SpecialCard
    {
        public override void OpenSpecialCard(Card card)
        {
            var randomChance = Random.Range(0, 2);
            var allCards = _levelCreator.Cards;

            var randomCard = allCards[Random.Range(0, allCards.Count)];

            if (randomChance > 0)
                OpeningPairCards(randomCard);
            else
                ClosingPairCards(randomCard);

            card.DelayHide();
        }
        
        private void OpeningPairCards(Card randomCard)
        {
            randomCard = GetRandomCard(randomCard, true);

            foreach (var card in _levelCreator.Cards.Where(card => card.NumberPair == randomCard.NumberPair))
                card.Show();
        }

        private void ClosingPairCards(Card randomCard)
        {
            var quantityMatchedCards = _levelCreator.Cards.Count(card => card.IsMatched);
            _gameWatcher.RemoveQuantityOfMatchedPairs();

            if (quantityMatchedCards > 0)
            {
                randomCard = GetRandomCard(randomCard, false);

                foreach (var card in _levelCreator.Cards.Where(card => card.NumberPair == randomCard.NumberPair))
                    card.Hide();
            }
            else
            {
                _gameWatcher.InitiateDefeat();
            }
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