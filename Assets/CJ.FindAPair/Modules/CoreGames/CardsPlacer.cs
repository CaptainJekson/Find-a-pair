using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames.Configs;
using DG.Tweening;
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
        
        public void DealCards(List<Card> cards)
        {
            List<Vector2> cardsPositions = new List<Vector2>();
            
            Sequence sequence = DOTween.Sequence();
            
            foreach (var card in cards)
            {
                cardsPositions.Add(card.transform.position);
                card.transform.position = _placeCardsConfig.CardsDeckPosition;
            }

            int currentInteraction = 0;
            
            foreach (var card in cards)
            {
                int i = currentInteraction;
                
                sequence.AppendInterval(_placeCardsConfig.TimeBetweenDeals);
                sequence.AppendCallback(() => card.Move(cardsPositions[i], _placeCardsConfig.CardDealSpeed, _placeCardsConfig.CardDealEase));
                
                currentInteraction++;
            }
        }
    }
}
