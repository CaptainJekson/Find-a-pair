using Code.Features.FindPairFeature.Components;
using Code.GlobalUtils;
using Scellecs.Morpeh;

namespace Code.Features.FindPairFeature.Systems
{
    public class FindPairTimerSystem : SimpleSystem<FindPairTime>, ISystem
    {
        [Injectable] private Stash<FindPairTime> _findPairTime;
        [Injectable] private Stash<FindPairDefeat> _findPairDefeat;
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var findPairTime = ref _findPairTime.Get(entity);
                
                findPairTime.value -= deltaTime;
                
                if (findPairTime.value > 0) continue;
                
                _findPairDefeat.Add(entity);
                _findPairTime.Remove(entity);
            }
        }
    }
}