using Code.Features.CleanupDestroyFeature;
using Code.Features.LevelCreateFeature;
using Code.Features.SaveGameFeature;
using Scellecs.Morpeh;

namespace Code
{
    public static class MorpehInitializer
    {
        public static void Initialize(World world, SimpleDImple container)
        {
            var groupIndex = 0;
            
            container.AddResolver(type => world.GetReflectionStash(type.GenericTypeArguments[0]), typeof(Stash));

            //Storages

            //TODO Services
            SaveGameFeature.Add(world, ref groupIndex, container);
            
            //TODO Core games
            LevelCreateFeature.Add(world, ref groupIndex, container);

            //Cleanup
            CleanupDestroyFeature.Add(world, ref groupIndex, container);
        }
    }
}