using Leopotam.Ecs;
using System;
using Zenject;

namespace CJ.FindAPair
{
    public class DefaultSystemsExecutor : ITickable, IDisposable
    {
        private readonly EcsWorld _world;
        private readonly EcsSystems _systems;

        public DefaultSystemsExecutor(EcsWorld world)
        {
            _world = world;
            _systems = new EcsSystems(_world, "DefaultSystems");

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif

            //_systems.Add(defaultSystem);

            _systems.Init();
        }

        public void Tick()
        {
            _systems.Run();
        }

        public void Dispose()
        {
            _systems.Destroy();
            _world.Destroy();
        }
    }
}
