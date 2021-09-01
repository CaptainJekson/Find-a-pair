﻿using System.Collections.Generic;
using CJ.FindAPair.Constants;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CJ.FindAPair.Modules.CoreGames
{
    public class LevelCreator : MonoBehaviour
    {
        [SerializeField] private Card card;

        private LevelConfig _level;
        private List<Card> _cards;
        private List<Card> _disableCards;

        public float Scale => _level.Scale;
        public LevelConfig LevelConfig => _level;
        public List<Card> Cards => _cards;

        public event UnityAction OnLevelCreated;
        public event UnityAction OnLevelDeleted;

        private void Awake()
        {
            _cards = new List<Card>();
            _disableCards = new List<Card>();
        }

        public void CreateLevel(LevelConfig level)
        {
            _level = level;
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

        private void PlaceCards() //TODO dev, потом выделить в отдельный класс
        {
            var startPosition = new Vector2(-1.3f, 2.5f); //Надо тоже как то высчитать в зависимости от ширины(width) и длины(height)
            var placePosition = startPosition;
            var heightBreakCounter = 0;
            var widthBreakCounter = 0;

            var offsetX = new Vector2(1.1f, 0); // Высчитать в зависимости от масштаба
            var offsetY = new Vector2(0, -1.1f);// Масштаб вычислить в зависимости от ширины(width) и длины(height)
            
            foreach (var isFilledCell in _level.LevelField)
            {
                var newCard = Instantiate(card, placePosition, Quaternion.identity, transform);
                newCard.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
                
                placePosition += offsetY;
                heightBreakCounter++;

                if (heightBreakCounter >= _level.Height)
                {
                    widthBreakCounter++;
                    placePosition = startPosition;
                    placePosition += offsetX * widthBreakCounter;
                    heightBreakCounter = 0;
                }
                
                AddCreatedCardToList(isFilledCell, newCard);
            }
        }

        private void AddCreatedCardToList(bool isFilledCell, Card newCard)
        {
            if (isFilledCell == false)
            {
                DisableCard(newCard);
                _disableCards.Add(newCard);
            }
            else
            {
                _cards.Add(newCard);
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
            card.MakeEmpty();
            card.NumberPair = 0;
        }
    }
}