using Code.Features.CleanupDestroyFeature.Components;
using Code.Features.FindPairFeature.Components;
using Code.GlobalUtils;
using Scellecs.Morpeh;
using UnityEngine;

namespace Code.Features.FindPairFeature.Systems
{
    public class FindPairVictorySystem : SimpleSystem<FindPairVictory, FindPairScore>, ISystem
    {
        [Injectable] private Stash<FindPairScore> _findPairScore;
        [Injectable] private Stash<Destroy> _destroy;
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var findPairScore = ref _findPairScore.Get(entity);
                var score = findPairScore.score;
                
                //TODO dev тут вся логика при выигрыше, начиление монет
                Debug.Log("ТЫ ВЫИГРАЛ!!!");
                _destroy.Add(entity);
            }
        }
    }
}