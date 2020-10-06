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

        public float ReductionRatio => _level.ReductionRatio;
        private GridLayoutGroup _gridLayoutGroup;

        private void Awake()
        {
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = _level.LevelConfig.Width;

            CreateLevel();
        }

        private void CreateLevel()  //TODO Refactor
        {
            var cards = new List<Card>();

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
                    cards.Add(newCard);
                }
            }

            for (int i = 0; i < cards.Count / 2; i++)   //TODO только для двойной пары
            {
                cards[i].NumberPair = i + 1;
                cards[cards.Count - i - 1].NumberPair = i + 1;
            }
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

