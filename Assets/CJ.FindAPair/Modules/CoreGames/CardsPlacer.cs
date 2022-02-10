using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.Service.Audio;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CJ.FindAPair.Modules.CoreGames
{
    public class CardsPlacer
    {
        private PlaceCardsConfig _placeCardsConfig;
        private AudioController _audioController;

        public event UnityAction CardsDealt;
        
        public CardsPlacer(PlaceCardsConfig placeCardsConfig, AudioController audioController)
        {
            _placeCardsConfig = placeCardsConfig;
            _audioController = audioController;
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
                newCard.AudioDriver = _audioController;

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
            
            int interactionsCounter = 0;
            
            foreach (var card in cards)
            {
                cardsPositions.Add(card.transform.position);
                card.transform.position = _placeCardsConfig.CardsDeckPosition;
            }

            foreach (var card in cards)
            {
                int i = interactionsCounter;
                
                sequence.AppendInterval(_placeCardsConfig.TimeBetweenDeals);
                sequence.AppendCallback(() =>
                {
                    card.Move(cardsPositions[i], _placeCardsConfig.CardDealSpeed, _placeCardsConfig.CardDealEase);
                    _audioController.PlaySound(_audioController.AudioClipsCollection.CardDealSound);
                });
                
                interactionsCounter++;
            }

            sequence.AppendInterval(_placeCardsConfig.DelayAfterCardsDealt);

            foreach (var card in cards)
            {
                sequence.AppendCallback(() => card.PlayAnimation(true));
            }
            
            sequence.AppendInterval(_placeCardsConfig.CardsShowingTime);
            sequence.AppendCallback(() => _audioController.PlayMusic(_audioController.AudioClipsCollection.OnLevelMusic));

            foreach (var card in cards)
            {
                sequence.AppendCallback(() => card.Hide());
            }
            
            sequence.AppendCallback(() => CardsDealt?.Invoke());
        }
    }
}