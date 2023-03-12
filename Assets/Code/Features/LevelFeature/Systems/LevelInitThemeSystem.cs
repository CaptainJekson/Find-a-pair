using Code.Features.LevelFeature.Components;
using Code.Features.ThemesFeature.Components;
using Code.GlobalUtils;
using Scellecs.Morpeh;

namespace Code.Features.LevelFeature.Systems
{
    public class LevelInitThemeSystem : SimpleSystem<Level, LevelInitialize>, ISystem
    {
        [Injectable] private Stash<ThemeLevelInit> _themeLevelInit;
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                _themeLevelInit.Add(entity);
            }
        }
    }
}