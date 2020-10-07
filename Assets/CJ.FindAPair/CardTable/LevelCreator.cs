using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.CardTable
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class LevelCreator : MonoBehaviour
    {
        [SerializeField] private Level _level;
        [SerializeField] private Card _card;

        private List<Card> _cards;

        public float ReductionRatio => _level.ReductionRatio;
        private GridLayoutGroup _gridLayoutGroup;

        private void Awake()
        {
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = _level.LevelConfig.Width;

            _cards = new List<Card>();

            CreateLevel();
            CardNumbering();
        }

        private void CreateLevel()  //TODO Refactor
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
                if(counter > 0)
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

        private List<Card> ShuffleCard(List<Card> cards)    //TODO
        {
            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }

            return cards;
        }

        private void DisableCard(Card card)
        {
            card.IsEmpty = true;
            card.GetComponent<Image>().enabled = false;
            card.GetComponent<Button>().interactable = false;
            card.NumberPair = 0;
        }
    }
}

