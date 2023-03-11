using Scellecs.Morpeh;

namespace Code.Features.LevelCreateFeature
{
    public static class LevelCreateFeature
    {
        public static void Add(World world, ref int index, SimpleDImple container)
        {
            var systemsGroup = world.CreateSystemsGroup();

            world.AddSystemsGroup(index++, systemsGroup);
        }
    }
}