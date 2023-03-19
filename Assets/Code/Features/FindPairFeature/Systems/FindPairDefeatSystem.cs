using Code.Features.FindPairFeature.Components;
using Code.GlobalUtils;
using Scellecs.Morpeh;
using UnityEngine;

namespace Code.Features.FindPairFeature.Systems
{
    public class FindPairDefeatSystem : SimpleSystem<FindPairDefeat>, ISystem
    {
        [Injectable] private Stash<FindPairDefeat> _findPairDefeat;
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                //TODO тут вся логика когда проигрываем
                
                //TODO не удаляем потому что может быть реклама
                Debug.LogError("ТЫ ПРОИГРАЛ!!!");
                _findPairDefeat.Remove(entity);
            }
        }
    }
}