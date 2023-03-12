using Code.Features.CleanupDestroyFeature.Components;
using Code.GlobalUtils;
using Scellecs.Morpeh;

namespace Code.Features.CleanupDestroyFeature.Systems
{
    public class EntityDestroySystem : SimpleSystem<Destroy>, ILateSystem
    {
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                entity.Dispose();
            }
        }
    }
}