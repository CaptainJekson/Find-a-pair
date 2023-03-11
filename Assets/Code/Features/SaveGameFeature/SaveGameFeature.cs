using Scellecs.Morpeh;

namespace Code.Features.SaveGameFeature
{
    public static class SaveGameFeature
    {
        public static void Add(World world, ref int index, SimpleDImple container)
        {
            var systemsGroup = world.CreateSystemsGroup();

            world.AddSystemsGroup(index++, systemsGroup);
        }
    }
}