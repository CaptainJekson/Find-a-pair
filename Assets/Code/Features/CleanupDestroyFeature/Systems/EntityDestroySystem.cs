using Code.Features.CleanupDestroyFeature.Components;
using Code.GlobalUtils;
using Scellecs.Morpeh;
using UnityEngine;

namespace Code.Features.CleanupDestroyFeature.Systems
{
    public class EntityDestroySystem : SimpleSystem<Destroy>, ILateSystem
    {
        public void OnUpdate(float deltaTime)
        {
            Debug.LogError("kek");
            
            foreach (var entity in _filter)
            {
                entity.Dispose();
            }
        }
    }
}