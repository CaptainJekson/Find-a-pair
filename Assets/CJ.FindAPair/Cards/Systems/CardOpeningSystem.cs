using CJ.FindAPair.Cards.Components;
using Leopotam.Ecs;

namespace CJ.FindAPair.Cards.Systems
{
    class CardOpeningSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CardComponent> _cardFilter;

        public void Run()
        {
            foreach (var i in _cardFilter)
            {
                var cardEntity = _cardFilter.Entities[i];
                var cardComponent = _cardFilter.Get1[i];

                cardComponent.Button.onClick.AddListener(() => OpeningCard(cardEntity));
            }
        }

        private void OpeningCard(EcsEntity cardEntity)
        {
            cardEntity.Set<OpenCardTag>();
        }
    }
}
