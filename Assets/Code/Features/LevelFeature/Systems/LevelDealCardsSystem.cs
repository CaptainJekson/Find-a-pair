using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames.Configs;
using Code.Features.LevelFeature.Components;
using Code.GlobalUtils;
using DG.Tweening;
using Scellecs.Morpeh;
using UnityEngine;

namespace Code.Features.LevelFeature.Systems
{
    public class LevelDealCardsSystem : SimpleSystem<Level, LevelInitialize>, ISystem
    {
        [Injectable] private Stash<Level> _level;
        
        [Injectable] private PlaceCardsConfig _placeCardsConfig;
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var level = ref _level.Get(entity);
                var cards = level.enableCards;
                
                var cardsPositions = new List<Vector2>();
                var sequence = DOTween.Sequence();
                var interactionsCounter = 0;
            
                foreach (var card in cards)
                {
                    cardsPositions.Add(card.transform.position);
                    card.transform.position = _placeCardsConfig.CardsDeckPosition;
                }

                foreach (var card in cards)
                {
                    var i = interactionsCounter;
                
                    sequence.AppendInterval(_placeCardsConfig.TimeBetweenDeals);
                    sequence.AppendCallback(() =>
                    {
                        card.Move(cardsPositions[i], _placeCardsConfig.CardDealSpeed, _placeCardsConfig.CardDealEase);
                        //_audioController.PlaySound(_audioController.AudioClipsCollection.CardDealSound); //TODO 
                    });
                
                    interactionsCounter++;
                }

                sequence.AppendInterval(_placeCardsConfig.DelayAfterCardsDealt);

                foreach (var card in cards)
                {
                    sequence.AppendCallback(() => card.PlayAnimation(true));
                }
            
                sequence.AppendInterval(_placeCardsConfig.CardsShowingTime);
                // sequence.AppendCallback(() => _audioController.PlayMusic(_themeConfigCollection
                //     .GetThemeConfig(_gameSaver.LoadData().ThemesData.SelectedTheme).Music)); //TODO 

                foreach (var card in cards)
                {
                    sequence.AppendCallback(() => card.Hide());
                }
            }
        }
    }
}