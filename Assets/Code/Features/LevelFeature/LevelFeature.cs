using Code.Features.LevelFeature.Components;
using Code.Features.LevelFeature.Interfaces;
using Code.Features.LevelFeature.Systems;
using Code.GlobalUtils;
using Scellecs.Morpeh;

namespace Code.Features.LevelFeature
{
    public static class LevelCreateFeature
    {
        public static void AddStorage(World world, ref int index, SimpleDImple container)
        {
            var systemsGroup = world.CreateSystemsGroup();

            systemsGroup.AddInitializer(container.NewAndRegister<LevelStorageSystem, ILevelStorage>());
            
            world.AddSystemsGroup(index++, systemsGroup);
        }
        
        public static void Add(World world, ref int index, SimpleDImple container)
        {
            var systemsGroup = world.CreateSystemsGroup();

            systemsGroup.AddSystem(container.New<LevelTESTStartSystem>());

            systemsGroup.AddSystem(container.New<LevelCreateSystem>());
            systemsGroup.AddSystem(container.New<LevelPlaceCardsSystem>());
            systemsGroup.AddSystem(container.New<LevelCardsNumberingSystem>());
            systemsGroup.AddSystem(container.New<LevelShuffleNumberCardSystem>());
            systemsGroup.AddSystem(container.New<LevelDealCardsSystem>());
            systemsGroup.AddSystem(container.New<LevelInitThemeSystem>());
            systemsGroup.AddSystem(container.New<LevelStartGameSystem>());
            systemsGroup.DeleteHere<LevelInitialize>();

            world.AddSystemsGroup(index++, systemsGroup);
        }
    }
}