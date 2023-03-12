using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using Code.Configs;
using Code.Features.LevelFeature.Components;
using Code.GlobalUtils;
using Scellecs.Morpeh;
using UnityEngine;

namespace Code.Features.LevelFeature.Systems
{
    public class LevelPlaceCardsSystem : SimpleSystem<Level, LevelInitialize>, ISystem
    {
        [Injectable] private Stash<Level> _level;

        [Injectable] private PlaceCardsConfig _placeCardsConfig;
        [Injectable] private TemplatesConfig _templates;
        [Injectable] private Locator _locator;
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var level = ref _level.Get(entity);
                
                level.cards = PlaceCards(level.levelConfig, _templates.card, _locator.tableCards);
                
                if (level.disableCards == null)
                {
                    level.disableCards = new List<Card>();
                }

                if (level.enableCards == null)
                {
                    level.enableCards = new List<Card>();
                }
                
                foreach (var card in level.cards)
                {
                    if (card.Value == false)
                    {
                        card.Key.IsEmpty = true;
                        card.Key.MakeEmpty();
                        card.Key.NumberPair = 0;

                        

                        level.disableCards.Add(card.Key);
                    }
                    else
                    {
                        level.enableCards.Add(card.Key);
                    }
                }
            }
        }
        
        private Dictionary<Card, bool> PlaceCards(LevelConfig level, Card cardPrefab, Transform parentTransform) //TODO dev
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
                //newCard.AudioDriver = _audioController; //TODO 

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