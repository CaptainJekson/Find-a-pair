using Code.Features.CleanupDestroyFeature.Systems;
using Scellecs.Morpeh;

namespace Code.Features.CleanupDestroyFeature
{
    public static class CleanupDestroyFeature
    {
        public static void Add(World world, ref int index, SimpleDImple container)
        {
            var systemsGroup = world.CreateSystemsGroup();

            systemsGroup.AddSystem(container.New<EntityDestroySystem>());

            world.AddSystemsGroup(index++, systemsGroup);
        }
    }
}