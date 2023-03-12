using Code.Features.FindPairFeature.Components;
using Code.Features.LevelFeature.Components;
using Code.GlobalUtils;
using Scellecs.Morpeh;

namespace Code.Features.LevelFeature.Systems
{
    public class LevelStartGameSystem : SimpleSystem<Level, LevelInitialize>, ISystem
    {
        [Injectable] private Stash<FindPairStart> _findPairStart;
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                _findPairStart.Add(World.CreateEntity());
            }
        }
    }
}