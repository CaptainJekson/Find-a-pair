using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CJ.FindAPair.Modules.CoreGames.SpecialCards
{
    public class EntanglementCard : SpecialCard
    {
        [SerializeField] private float _cardsMoveSpeed;
        [SerializeField] private Ease _moveType;

        public override void OpenSpecialCard(Card specialCardOld)
        {
            ShuffleCards();
        }

        private void ShuffleCards()
        {
            List<Vector3> cardsPositions = new List<Vector3>();

            foreach (var card in _levelCreator.Cards)
            {
                cardsPositions.Add(card.transform.position);
            }

            for (int i = cardsPositions.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i);

                Vector3 temp = cardsPositions[i];
                cardsPositions[i] = cardsPositions[j];
                cardsPositions[j] = temp;
            }

            for (int i = 0; i < _levelCreator.Cards.Count; i++)
            {
                _levelCreator.Cards[i].MoveCard(cardsPositions[i], _cardsMoveSpeed, _moveType);
            }
        }
    }
}