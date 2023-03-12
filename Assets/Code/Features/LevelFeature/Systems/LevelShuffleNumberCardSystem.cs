using Code.Features.LevelFeature.Components;
using Code.GlobalUtils;
using Scellecs.Morpeh;
using UnityEngine;

namespace Code.Features.LevelFeature.Systems
{
    public class LevelShuffleNumberCardSystem : SimpleSystem<Level, LevelInitialize>, ISystem
    {
        [Injectable] private Stash<Level> _level;
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var level = ref _level.Get(entity);
                var enableCards = level.enableCards;
                
                for (var i = enableCards.Count - 1; i > 0; i--)
                {
                    var j = Random.Range(0, i);
                    (enableCards[i].NumberPair, enableCards[j].NumberPair) 
                        = (enableCards[j].NumberPair, enableCards[i].NumberPair);
                }
            }
        }
    }
}