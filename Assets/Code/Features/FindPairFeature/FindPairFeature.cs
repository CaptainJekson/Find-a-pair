using Code.Features.FindPairFeature.Components;
using Code.Features.FindPairFeature.Systems;
using Code.GlobalUtils;
using Scellecs.Morpeh;

namespace Code.Features.FindPairFeature
{
    public static class FindPairFeature
    {
        public static void Add(World world, ref int index, SimpleDImple container)
        {
            var systemsGroup = world.CreateSystemsGroup();

            systemsGroup.AddSystem(container.New<FindPairStartGameSystem>());
            systemsGroup.DeleteHere<FindPairStart>();

            systemsGroup.AddSystem(container.New<FindPairTimerSystem>());
            systemsGroup.AddSystem(container.New<FindPairLifeTakeSystem>());
            systemsGroup.AddSystem(container.New<FindPairScoreTakeSystem>());
            
            systemsGroup.AddSystem(container.New<FindPairScoreGiveSystem>());
            systemsGroup.AddSystem(container.New<FindPairQuantityPairsGiveSystem>());
            systemsGroup.DeleteHere<FindPairScoreGive>();

            systemsGroup.AddSystem(container.New<FindPairComparerSystem>());
            systemsGroup.AddSystem(container.New<FindPairVictorySystem>());
            systemsGroup.AddSystem(container.New<FindPairDefeatSystem>());

            world.AddSystemsGroup(index++, systemsGroup);
        }
    }
}