using Code.Features.ThemesFeature.Systems;
using Scellecs.Morpeh;

namespace Code.Features.ThemesFeature
{
    public static class ThemesFeature
    {
        public static void Add(World world, ref int index, SimpleDImple container)
        {
            var systemsGroup = world.CreateSystemsGroup();

            systemsGroup.AddSystem(container.New<ThemeInitSystem>());

            world.AddSystemsGroup(index++, systemsGroup);
        }
    }
}