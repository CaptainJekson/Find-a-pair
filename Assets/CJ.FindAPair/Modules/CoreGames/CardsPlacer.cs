using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames.Configs;
using UnityEngine;

namespace CJ.FindAPair.Modules.CoreGames
{
    public class CardsPlacer
    {
        private PlaceCardsConfig _placeCardsConfig;

        public CardsPlacer(PlaceCardsConfig placeCardsConfig)
        {
            _placeCardsConfig = placeCardsConfig;
        }

        public Dictionary<Card, bool> PlaceCards(LevelConfig level, Card cardPrefab, Transform parentTransform) //TODO dev
        {
            var cards = new Dictionary<Card, bool>();
            var startPosition = _placeCardsConfig.GetStartPosition(level.Width, level.Height, level.HeightOffset);
            var placePosition = startPosition;
            var heightBreakCounter = 0;
            var widthBreakCounter = 0;

            var scale = _placeCardsConfig.GetScale(level.Width, level.Height);

            var offsetPositionX = new Vector2((scale * 2 + scale * _placeCardsConfig.OffsetFactorX), 0); 
            var offsetPositionY = new Vector2(0, (-scale * 2 + scale * -_placeCardsConfig.OffsetFactorY));
            
            foreach (var isFilledCell in level.LevelField)
            {
                var newCard = Object.Instantiate(cardPrefab, placePosition, Quaternion.identity, parentTransform);
                newCard.transform.localScale = Vector3.one * scale;  
                
                placePosition += offsetPositionY;
                heightBreakCounter++;

                if (heightBreakCounter >= level.Height)
                {
                    widthBreakCounter++;
                    placePosition = startPosition;
                    placePosition += offsetPositionX * widthBreakCounter;
                    heightBreakCounter = 0;
                }
                
                cards.Add(newCard, isFilledCell);
            }

            return cards;
        }
    }
}
