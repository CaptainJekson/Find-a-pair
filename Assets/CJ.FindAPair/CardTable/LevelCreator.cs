﻿using UnityEngine;
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

        private void CreateLevel()
        {
            foreach (var item in _level.LevelConfig.LevelField)
            {
                var newCard = Instantiate(_card, transform.position, Quaternion.identity);

                newCard.transform.SetParent(transform, false);

                if (item == false)
                {
                    newCard.IsEmpty = true;
                    newCard.GetComponent<Image>().enabled = false;
                    newCard.GetComponent<Button>().interactable = false;
                }
            }
        }
    }
}
