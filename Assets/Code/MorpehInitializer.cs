using Code.Features.CleanupDestroyFeature;
using Code.Features.FindPairFeature;
using Code.Features.LevelFeature;
using Code.Features.SaveGameFeature;
using Code.Features.ThemesFeature;
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
            LevelCreateFeature.AddStorage(world, ref groupIndex, container);

            //Systems
            //TODO Services
            SaveGameFeature.Add(world, ref groupIndex, container);
            
            //TODO Core game
            LevelCreateFeature.Add(world, ref groupIndex, container);
            FindPairFeature.Add(world, ref groupIndex, container);
            
            //TODO meta game
            ThemesFeature.Add(world, ref groupIndex, container);

            //Cleanup
            CleanupDestroyFeature.Add(world, ref groupIndex, container);
        }
    }
}