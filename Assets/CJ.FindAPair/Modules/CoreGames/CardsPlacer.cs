using System;
using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames.Configs;
using UnityEngine;

namespace CJ.FindAPair.Modules.CoreGames
{
    public class CardsPlacer : MonoBehaviour
    {
        private Dictionary<Card, bool> _cards;

        private void Awake()
        {
            _cards = new Dictionary<Card, bool>();
        }

        public Dictionary<Card, bool> PlaceCards(LevelConfig level, Card cardPrefab) //TODO dev
        {
            var startPosition = new Vector2(-1.3f, 2.5f); //Надо тоже как то высчитать в зависимости от ширины(width) и длины(height)
            var placePosition = startPosition;
            var heightBreakCounter = 0;
            var widthBreakCounter = 0;

            var offsetX = new Vector2(1.1f, 0); // Высчитать в зависимости от масштаба
            var offsetY = new Vector2(0, -1.1f);// Масштаб вычислить в зависимости от ширины(width) и длины(height)
            
            foreach (var isFilledCell in level.LevelField)
            {
                var newCard = Instantiate(cardPrefab, placePosition, Quaternion.identity, transform);
                newCard.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
                
                placePosition += offsetY;
                heightBreakCounter++;

                if (heightBreakCounter >= level.Height)
                {
                    widthBreakCounter++;
                    placePosition = startPosition;
                    placePosition += offsetX * widthBreakCounter;
                    heightBreakCounter = 0;
                }
                
                _cards.Add(newCard, isFilledCell);
            }

            return _cards;
        }
    }
}
