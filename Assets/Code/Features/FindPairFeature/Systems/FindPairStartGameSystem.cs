using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using Code.Features.FindPairFeature.Components;
using Code.Features.LevelFeature.Interfaces;
using Code.GlobalUtils;
using Scellecs.Morpeh;

namespace Code.Features.FindPairFeature.Systems
{
    public class FindPairStartGameSystem : SimpleSystem<FindPairStart>, ISystem
    {
        [Injectable] private Stash<FindPairStart> _findPairStart;
        [Injectable] private Stash<FindPairLife> _findPairLife;
        [Injectable] private Stash<FindPairScore> _findPairScore;
        [Injectable] private Stash<FindPairTime> _findPairTime;
        [Injectable] private Stash<FindPairQuantityPairs> _findPairQuantityPairs;
        [Injectable] private Stash<FindPairComparisonCards> _findPairComparisonCards;
        
        [Injectable] private ILevelStorage _levelStorage;
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                if (!_levelStorage.TryGetCurrentLevel(out var levelConfig)) continue;
                
                var cards = _levelStorage.GetCards();
                    
                var quantityOfPairs = (cards.Count / (int)levelConfig.QuantityOfCardOfPair)
                                      - levelConfig.QuantityPairOfSpecialCard;

                _findPairQuantityPairs.Set(entity, new FindPairQuantityPairs
                {
                    quantityOfCardOfPair = (int)levelConfig.QuantityOfCardOfPair,
                    maxQuantityPairs = quantityOfPairs,
                });
                _findPairLife.Set(entity, new FindPairLife
                {
                    value = levelConfig.Tries,
                });
                _findPairTime.Set(entity, new FindPairTime
                {
                    value = levelConfig.Time,
                });
                _findPairScore.Set(entity, new FindPairScore
                {
                    score = 0,
                });
                _findPairComparisonCards.Set(entity, new FindPairComparisonCards
                {
                    cards = new List<Card>(),
                });
            }
        }
    }
}