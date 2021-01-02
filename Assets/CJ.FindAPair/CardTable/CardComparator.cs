using System.Collections.Generic;
using CJ.FindAPair.Constants;
using UnityEngine;
using UnityEngine.Events;

namespace CJ.FindAPair.CardTable
{
    [RequireComponent(typeof(LevelCreator))]
    public class CardComparator : MonoBehaviour
    {
        private LevelCreator _levelCreator;
        private List<Card> _comparisonCards;

        public event UnityAction CardsMatched;
        public event UnityAction CardsNotMatched;
        public event UnityAction<Card> SpecialCardOpened;

        private void Awake()
        {
            _levelCreator = GetComponent<LevelCreator>();
            _comparisonCards = new List<Card>();
        }

        private void OnEnable()
        {
            _levelCreator.OnLevelCreated += SubscriptionCards;
            _levelCreator.OnLevelDeleted += UnsubscriptionCards;
        }

        private void OnDisable()
        {
            _levelCreator.OnLevelCreated -= SubscriptionCards;
            _levelCreator.OnLevelDeleted -= UnsubscriptionCards;
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

        private void ToCompare(Card card)
        {
            if (card.NumberPair >= ConstantsCard.NUMBER_SPECIAL)
            {
                SpecialCardOpened?.Invoke(card);
            }

            var quantityOfCardOfPair = (int) _levelCreator.LevelConfig.QuantityOfCardOfPair;

            if (card.NumberPair < ConstantsCard.NUMBER_SPECIAL)
                _comparisonCards.Add(card);

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
                card.IsMatched = true;

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