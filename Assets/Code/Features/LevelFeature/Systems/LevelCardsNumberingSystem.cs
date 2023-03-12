using Code.Features.LevelFeature.Components;
using Code.GlobalUtils;
using Scellecs.Morpeh;

namespace Code.Features.LevelFeature.Systems
{
    public class LevelCardsNumberingSystem : SimpleSystem<Level, LevelInitialize>, ISystem
    {
        [Injectable] private Stash<Level> _level;
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var level = ref _level.Get(entity);
                var enableCards = level.enableCards;
                
                var numberCard = 1;
                var counter = (int) level.levelConfig.QuantityOfCardOfPair;

                for (var i = 0; i < enableCards.Count; i++)
                {
                    if (counter > 0)
                    {
                        enableCards[i].NumberPair = numberCard;
                        --counter;
                    }
                    else
                    {
                        counter = (int) level.levelConfig.QuantityOfCardOfPair;
                        ++numberCard;
                        --i;
                    }
                }
            }
        }
    }
}