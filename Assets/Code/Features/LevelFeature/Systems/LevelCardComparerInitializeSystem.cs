using Code.Features.FindPairFeature.Systems;
using Code.Features.LevelFeature.Components;
using Code.GlobalUtils;
using Scellecs.Morpeh;

namespace Code.Features.LevelFeature.Systems
{
    public class LevelCardComparerInitializeSystem : SimpleSystem<Level, LevelInitialize>, ISystem
    {
        [Injectable] private Stash<Level> _level;
        [Injectable] private Stash<CardComparerEvent> _findPairCardComparer;
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var level = ref _level.Get(entity);

                foreach (var card in level.cards)
                {
                    if(!card.Value) continue;
                    
                    card.Key.СardOpens += () =>
                    {
                        var comparerEntity = World.CreateEntity();
                        _findPairCardComparer.Set(comparerEntity, new CardComparerEvent
                        {
                            card = card.Key,
                        });
                    };
                }
            }
        }
    }
}