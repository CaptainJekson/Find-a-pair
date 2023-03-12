using System.Collections.Generic;
using CJ.FindAPair.Constants;
using UnityEngine.Events;

namespace CJ.FindAPair.Modules.CoreGames
{
    public class CardComparator
    {
        private LevelCreator _levelCreator;
        private List<Card> _comparisonCards;

        public List<Card> ComparisonCards => _comparisonCards;

        public event UnityAction CardsMatched;
        public event UnityAction CardsNotMatched;
        public event UnityAction<Card> SpecialCardOpened;
        
        public CardComparator(LevelCreator levelCreator)
        {
            _levelCreator = levelCreator;
            _comparisonCards = new List<Card>();
            _levelCreator.LevelCreated += SubscriptionCards;
            _levelCreator.LevelDeleted += UnsubscriptionCards;
        }
        
        private void SubscriptionCards()
        {
            foreach (var card in _levelCreator.Cards)
            {
                card.СardOpens += AddCardCompare(card);
            }
        }

        private void UnsubscriptionCards()
        {
            _comparisonCards.Clear();

            foreach (var card in _levelCreator.Cards)
            {
                card.СardOpens -= AddCardCompare(card);
            }
        }

        private UnityAction AddCardCompare(Card card)
        {
            return () => ToCompare(card);
        }

        private void ToCompare(Card cardOld)
        {
            if (cardOld.NumberPair >= ConstantsCard.NUMBER_SPECIAL)
            {
                SpecialCardOpened?.Invoke(cardOld);
            }

            var quantityOfCardOfPair = (int) _levelCreator.LevelConfig.QuantityOfCardOfPair;

            if (cardOld.NumberPair < ConstantsCard.NUMBER_SPECIAL)
                _comparisonCards.Add(cardOld);

            for (var i = 0; i < _comparisonCards.Count - 1; i++)
            {
                var isCardEqual = _comparisonCards[i].NumberPair
                                  == _comparisonCards[_comparisonCards.Count - 1].NumberPair;

                if (isCardEqual)
                {
                    if (_comparisonCards.Count < quantityOfCardOfPair)
                        continue;

                    OnCardsMatched();
                }
                else
                {
                    OnCardsNotMatched();
                }
            }
        }
        
        private void OnCardsMatched()
        {
            CardsMatched?.Invoke();

            foreach (var card in _comparisonCards)
                card.SetMatchedState();

            _comparisonCards.Clear();
        }
        
        private void OnCardsNotMatched()
        {
            CardsNotMatched?.Invoke();
            HideCards();
            _comparisonCards.Clear();
        }

        private void HideCards()
        {
            foreach (var comparisonCard in _comparisonCards)
            {
                comparisonCard.DelayHide();
            }
        }
    }
}