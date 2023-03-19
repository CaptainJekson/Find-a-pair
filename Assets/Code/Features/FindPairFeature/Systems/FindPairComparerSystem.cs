using CJ.FindAPair.Constants;
using Code.Features.FindPairFeature.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Code.Features.FindPairFeature.Systems
{
    public class FindPairComparerSystem : ISystem
    {
        [Injectable] private Stash<CardComparerEvent> _cardComparerEvent;
        [Injectable] private Stash<FindPairComparisonCards> _findPairComparisonCards;
        [Injectable] private Stash<FindPairQuantityPairs> _findPairQuantityPairs;
        [Injectable] private Stash<FindPairScoreGive> _findPairScoreGive;
        [Injectable] private Stash<FindPairLifeTake> _findPairLifeTake;

        private Filter _eventFilter;
        private Filter _comparisonFilter;
        
        public World World { get; set; }
        
        public void OnAwake()
        {
            _eventFilter = World.Filter
                .With<CardComparerEvent>();

            _comparisonFilter = World.Filter
                .With<FindPairComparisonCards>()
                .With<FindPairQuantityPairs>();
        }

        
        public void OnUpdate(float deltaTime)
        {
            foreach (var eventEntity in _eventFilter)
            {
                ref var cardComparerEvent = ref _cardComparerEvent.Get(eventEntity);
                var card = cardComparerEvent.card;
                
                foreach (var entity in _comparisonFilter)
                {
                    ref var findPairQuantityPairs = ref _findPairQuantityPairs.Get(entity);
                    ref var findPairComparisonCards = ref _findPairComparisonCards.Get(entity);
                    var comparisonCards = findPairComparisonCards.cards;

                    if (card.NumberPair >= ConstantsCard.NUMBER_SPECIAL)
                    {
                        //TODO потом слелаем когда будем делать специальыне карты
                        //SpecialCardOpened?.Invoke(card);
                    }
                    
                    var quantityOfCardOfPair = findPairQuantityPairs.quantityOfCardOfPair;
                    
                    if (card.NumberPair < ConstantsCard.NUMBER_SPECIAL)
                    {
                        comparisonCards.Add(card);
                    }
                    
                    for (var i = 0; i < comparisonCards.Count - 1; i++)
                    {
                        var isCardEqual = comparisonCards[i].NumberPair == comparisonCards[^1].NumberPair;

                        if (isCardEqual)
                        {
                            if (comparisonCards.Count < quantityOfCardOfPair) continue;

                            foreach (var comparisonCard in comparisonCards)
                            {
                                comparisonCard.SetMatchedState();
                            }

                            comparisonCards.Clear();
                            
                            _findPairScoreGive.Set(entity);
                        }
                        else
                        {
                            _findPairLifeTake.Add(entity);
                            
                            foreach (var comparisonCard in comparisonCards)
                            {
                                comparisonCard.DelayHide();
                            }
                            
                            comparisonCards.Clear();
                        }
                    }
                }
                
                _cardComparerEvent.Remove(eventEntity);
            }
        }

        public void Dispose()
        {
            _eventFilter = null;
            _comparisonFilter = null;
        }
    }
}