using System.Collections.Generic;
using CJ.FindAPair.Constants;
using CJ.FindAPair.Modules.CoreGames.Configs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CJ.FindAPair.Modules.CoreGames
{
    public class LevelCreator : MonoBehaviour
    {
        [SerializeField] private Card _card;

        private LevelConfig _level;
        private List<Card> _cards;
        private List<Card> _disableCards;
        private GridLayoutGroup _gridLayoutGroup;

        public float Scale => _level.Scale;
        public LevelConfig LevelConfig => _level;
        public List<Card> Cards => _cards;

        public event UnityAction OnLevelCreated;
        public event UnityAction OnLevelDeleted;

        private void Awake()
        {
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;

            _cards = new List<Card>();
            _disableCards = new List<Card>();
        }

        public void CreateLevel(LevelConfig level)
        {
            _level = level;

            _gridLayoutGroup.constraintCount = _level.Width;

            PlaceCards();
            CardNumbering();
            AddAllSpecialCards();
            ShuffleNumberCard();

            OnLevelCreated?.Invoke();
        }

        public void ClearLevel()
        {
            foreach (var card in _cards)
            {
                Destroy(card.gameObject);
            }
            
            foreach (var card in _disableCards)
            {
                Destroy(card.gameObject);
            }
            
            _cards.Clear();
            _disableCards.Clear();
            
            OnLevelDeleted?.Invoke();
        }
        
        public void RestartLevel()
        {
            ClearLevel();
            CreateLevel(_level);
        }

        private void PlaceCards()
        {
            foreach (var cell in _level.LevelField)
            {
                var newCard = Instantiate(_card, transform);

                if (cell == false)
                {
                    DisableCard(newCard);
                    _disableCards.Add(newCard);
                }
                else
                {
                    _cards.Add(newCard);
                }
            }
        }

        private void CardNumbering()
        {
            var numberCard = 1;
            var counter = (int) _level.QuantityOfCardOfPair;

            for (var i = 0; i < _cards.Count; i++)
            {
                if (counter > 0)
                {
                    _cards[i].NumberPair = numberCard;
                    --counter;
                }
                else
                {
                    counter = (int) _level.QuantityOfCardOfPair;
                    ++numberCard;
                    --i;
                }
            }
        }

        private void ShuffleNumberCard()
        {
            for (var i = _cards.Count - 1; i > 0; i--)
            {
                var j = Random.Range(0, i);
                var temp = _cards[i].NumberPair;
                _cards[i].NumberPair = _cards[j].NumberPair;
                _cards[j].NumberPair = temp;
            }
        }

        private void AddAllSpecialCards()
        {
            AddSpecialCards(_level.QuantityPairOfFortune, ConstantsCard.NUMBER_FORTUNE);
            AddSpecialCards(_level.QuantityPairOfEntanglement, ConstantsCard.NUMBER_ENTANGLEMENT);
            AddSpecialCards(_level.QuantityPairOfReset, ConstantsCard.NUMBER_RESET);
            AddSpecialCards(_level.QuantityPairOfBombs, ConstantsCard.NUMBER_BOMB);
        }
        
        private void AddSpecialCards(int quantityPairOfSpecial, int number)
        {
            var quantitySpecialCards = quantityPairOfSpecial * (int) _level.QuantityOfCardOfPair;

            for (var i = 0; i < quantitySpecialCards; i++)
            {
                _cards[_cards.Count - 1 - i].NumberPair = number;
            }
        }

        private void DisableCard(Card card)
        {
            card.IsEmpty = true;
            card.GetComponent<Image>().enabled = false;
            card.NumberPair = 0;
        }
    }
}