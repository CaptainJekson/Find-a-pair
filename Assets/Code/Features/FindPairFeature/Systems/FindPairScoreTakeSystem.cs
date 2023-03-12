using Code.Features.FindPairFeature.Components;
using Code.GlobalUtils;
using Scellecs.Morpeh;

namespace Code.Features.FindPairFeature.Systems
{
    public class FindPairScoreTakeSystem : SimpleSystem<FindPairScore, FindPairScoreTake>, ISystem
    {
        [Injectable] private Stash<FindPairScore> _findPairScore;
        [Injectable] private Stash<FindPairScoreTake> _findPairScoreTake;
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var findPairScore = ref _findPairScore.Get(entity);
                findPairScore.score -= findPairScore.accruedScore;
                
                _findPairScoreTake.Remove(entity);
            }
        }
    }
}