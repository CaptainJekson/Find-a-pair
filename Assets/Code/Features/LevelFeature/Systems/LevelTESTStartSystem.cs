using CJ.FindAPair.Modules.CoreGames.Configs;
using Code.Features.LevelFeature.Components;
using Scellecs.Morpeh;

namespace Code.Features.LevelFeature.Systems
{
    public class LevelTESTStartSystem : ISystem
    {
        [Injectable] private Stash<LevelCreate> _levelCreate;
        
        [Injectable] private LevelConfigCollection _levelConfigCollection;

        private const int SelectedLevel = 3;
        
        public World World { get; set; }

        public void OnAwake()
        {
            var level = _levelConfigCollection.Levels[SelectedLevel - 1];

            var newEntity = World.CreateEntity();
            _levelCreate.Set(newEntity, new LevelCreate
            {
                levelConfig = level,
            }); 
        }

        public void OnUpdate(float deltaTime)
        {

        }

        public void Dispose()
        {

        }
    }
}