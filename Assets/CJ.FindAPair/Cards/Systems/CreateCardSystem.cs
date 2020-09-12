using CJ.FindAPair.Cards.Configs;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.Cards.Systems
{
    public class CreateCardSystem : IEcsInitSystem
    {
        private readonly LevelConfigGroup _levelConfigGroup;

        private EcsWorld _world;

        public CreateCardSystem(LevelConfigGroup levelConfigGroup)
        {
            _levelConfigGroup = levelConfigGroup;
        }

        public void Init()
        {
            CreateCards(_levelConfigGroup, 0);
        }

        private void CreateCards(LevelConfigGroup levelConfigGroup, int level)
        {
            for (int i = 0; i < levelConfigGroup.LevelConfigs[level].PositionCard.Count; i++)
            {
                var cardComponent = _world.NewEntity().Set<CardComponent>();

                Vector3 position = new Vector3(levelConfigGroup.LevelConfigs[level].PositionCard[i].x,
                    levelConfigGroup.LevelConfigs[level].PositionCard[i].y, 0.0f);

                var table = Object.FindObjectOfType<CardTableReference>();
                var newCard = Object.Instantiate(levelConfigGroup.CardPrefab, position, Quaternion.identity);
                var scaleCard = levelConfigGroup.LevelConfigs[level].ScaleCard;

                cardComponent.Number = i;
                cardComponent.Button = newCard.GetComponent<Button>();

                newCard.gameObject.transform.localScale = new Vector3(scaleCard, scaleCard, scaleCard);
                
                newCard.transform.SetParent(table.transform, false);
            }
        }
    }
}


