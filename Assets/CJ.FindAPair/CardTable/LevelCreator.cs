using Assets.CJ.FindAPair.Constants;
using CJ.FindAPair.Configuration;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CJ.FindAPair.CardTable
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class LevelCreator : MonoBehaviour
    {
        [SerializeField] private Card _card;

        private Level _level;
        private List<Card> _cards;
        private GridLayoutGroup _gridLayoutGroup;

        public float ReductionRatio => _level.ReductionRatio;
        public LevelConfig LevelConfig => _level.LevelConfig;
        public List<Card> Cards => _cards;

        public event UnityAction OnLevelCreated;  

        private void Awake()
        {
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;

            _cards = new List<Card>();           
        }

        public void CreateLevel(Level level)
        {
            _level = level;
            _gridLayoutGroup.constraintCount = _level.LevelConfig.Width;

            PlaceCards();
            CardNumbering();
            AddBombs();
            ShuffleNumberCard();

            OnLevelCreated?.Invoke();
        }

        private void PlaceCards()
        {
            for (var i = 0; i < _level.LevelConfig.LevelField.Count; i++)
            {
                var newCard = Instantiate(_card, transform.position, Quaternion.identity);

                newCard.transform.SetParent(transform, false);

                if (_level.LevelConfig.LevelField[i] == false)
                {
                    DisableCard(newCard);
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
            var counter = (int)_level.LevelConfig.QuantityOfCardOfPair;

            for (int i = 0; i < _cards.Count; i++)
            {
                if (counter > 0)
                {
                    _cards[i].NumberPair = numberCard;
                    --counter;
                }
                else
                {
                    counter = (int)_level.LevelConfig.QuantityOfCardOfPair;
                    ++numberCard;
                    --i;
                }
            }
        }

        private void ShuffleNumberCard()
        {
            for (int i = _cards.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i);
                int temp = _cards[i].NumberPair;
                _cards[i].NumberPair = _cards[j].NumberPair;
                _cards[j].NumberPair = temp;
            }
        }

        private void AddBombs()
        {
            var quantityBomb = _level.LevelConfig.QuantityPairOfBombs * (int)_level.LevelConfig.QuantityOfCardOfPair;

            for (int i = 0; i < quantityBomb; i++)
            {
                _cards[_cards.Count - 1 - i].NumberPair = Constants.NumberBomb;
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

