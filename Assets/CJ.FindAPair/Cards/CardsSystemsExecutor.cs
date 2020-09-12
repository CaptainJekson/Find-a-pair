using CJ.FindAPair.Cards.Systems;
using Leopotam.Ecs;
using System;
using Zenject;

namespace CJ.FindAPair.Cards
{
    class CardsSystemsExecutor : ITickable, IDisposable
    {
        private readonly EcsSystems _systems;
        public CardsSystemsExecutor(EcsWorld world, CreateCardSystem createCardSystem, 
            CardOpeningSystem cardOpeningSystem)
        {
            _systems = new EcsSystems(world, "PlayerSystems");

            _systems
                .Add(createCardSystem)
                .Add(cardOpeningSystem);

            _systems.Init();

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
        }

        public void Tick()
        {
            _systems.Run();
        }

        public void Dispose()
        {
            _systems.Destroy();
        }
    }
}
