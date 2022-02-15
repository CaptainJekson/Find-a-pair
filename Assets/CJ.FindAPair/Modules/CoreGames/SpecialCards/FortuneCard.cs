﻿using System.Linq;
using CJ.FindAPair.Constants;
using UnityEngine;
using UnityEngine.Events;

namespace CJ.FindAPair.Modules.CoreGames.SpecialCards
{
    public class FortuneCard : SpecialCard
    {
        private bool _isHundredPercentOpening;
        
        public event UnityAction CardRealised;

        public void MakeOneHundredPercentOpening()
        {
            _isHundredPercentOpening = true;
        }
        
        public override void OpenSpecialCard(Card specialCard)
        {
            Debug.LogError(_isHundredPercentOpening + " ---  " + GetHashCode());
            var randomChance = _isHundredPercentOpening ? 1 : Random.Range(0, 2);
            var randomCard = _levelCreator.Cards[Random.Range(0, _levelCreator.Cards.Count)];

            if (randomChance > 0)
                OpeningPairCards(randomCard);
            else
                ClosingPairCards(randomCard);

            specialCard.DelayHide();
            MakeChanceInDefaultState();
        }

        private void MakeChanceInDefaultState()
        {
            if(_isHundredPercentOpening)
                _isHundredPercentOpening = false;
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
                CardRealised?.Invoke();
            }
        }
        
        private Card GetRandomCard(Card randomCardOld, bool isMatched)
        {
            while (!(isMatched ^ randomCardOld.IsMatched) || randomCardOld.NumberPair >= ConstantsCard.NUMBER_SPECIAL)
            {
                randomCardOld = _levelCreator.Cards[Random.Range(0, _levelCreator.Cards.Count)];
            }

            return randomCardOld;
        }
    }
}