using Code.Features.FindPairFeature.Components;
using Code.GlobalUtils;
using Scellecs.Morpeh;

namespace Code.Features.FindPairFeature.Systems
{
    public class FindPairLifeTakeSystem : SimpleSystem<FindPairLife, FindPairScore, FindPairLifeTake>, ISystem
    {
        [Injectable] private Stash<FindPairLife> _findPairLife;
        [Injectable] private Stash<FindPairLifeTake> _findPairLifeTake;
        [Injectable] private Stash<FindPairScore> _findPairScore;
        [Injectable] private Stash<FindPairDefeat> _findPairDefeat;
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var findPairLife = ref _findPairLife.Get(entity);
                ref var findPairScore = ref _findPairScore.Get(entity);

                findPairLife.value--;
                findPairScore.comboCounter = 0;

                if (findPairLife.value <= 0)
                {
                    _findPairDefeat.Add(entity);
                }
                
                _findPairLifeTake.Remove(entity);
            }
        }
    }
}