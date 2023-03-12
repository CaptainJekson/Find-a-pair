using Code.Features.LevelFeature.Components;
using Code.GlobalUtils;
using Scellecs.Morpeh;

namespace Code.Features.LevelFeature.Systems
{
    public class LevelCreateSystem : SimpleSystem<LevelCreate>, ISystem
    {
        [Injectable] private Stash<LevelCreate> _levelCreate;
        [Injectable] private Stash<Level> _level;
        [Injectable] private Stash<LevelInitialize> _levelInitialize;

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var levelCreate = ref _levelCreate.Get(entity);
                _level.Set(entity, new Level
                {
                    levelConfig = levelCreate.value,
                });
                _levelInitialize.Add(entity);
                _levelCreate.Remove(entity);
            }
        }
    }
}