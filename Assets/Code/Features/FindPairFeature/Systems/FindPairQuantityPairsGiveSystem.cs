using Code.Features.FindPairFeature.Components;
using Code.GlobalUtils;
using Scellecs.Morpeh;

namespace Code.Features.FindPairFeature.Systems
{
    public class FindPairQuantityPairsGiveSystem : SimpleSystem<FindPairQuantityPairs, FindPairScoreGive>, ISystem
    {
        [Injectable] private Stash<FindPairQuantityPairs> _findPairQuantityPairs;
        [Injectable] private Stash<FindPairVictory> _findPairVictory;
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var findPairQuantityPairs = ref _findPairQuantityPairs.Get(entity);
                findPairQuantityPairs.matchedQuantityPairs++;

                if (findPairQuantityPairs.matchedQuantityPairs >= findPairQuantityPairs.maxQuantityPairs)
                {
                    _findPairVictory.Add(entity);
                }
            }
        }
    }
}