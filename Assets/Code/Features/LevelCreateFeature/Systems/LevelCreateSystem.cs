using Code.Features.LevelCreateFeature.Components;
using Code.GlobalUtils;
using Scellecs.Morpeh;

namespace Code.Features.LevelCreateFeature.Systems
{
    public class LevelCreateSystem : SimpleSystem<LevelCreate>, ISystem
    {
        [Injectable] private Stash<LevelCreate> _levelCreate;
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                
                _levelCreate.Remove(entity);
            }
        }
    }
}