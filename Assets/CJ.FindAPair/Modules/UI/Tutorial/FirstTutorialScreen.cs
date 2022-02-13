using CJ.FindAPair.Modules.UI.Tutorial.Base;
using UnityEngine;

namespace CJ.FindAPair.Modules.UI.Tutorial
{
    public class FirstTutorialScreen : TutorialScreen
    {
        [SerializeField] private RectTransform _tapZoneViewOneCard;
        [SerializeField] private RectTransform _tapZoneOneCard;
        
        [SerializeField] private RectTransform _tapZoneViewTwoCard;
        [SerializeField] private RectTransform _tapZoneTwoCard;

        [SerializeField] private RectTransform _tapZoneViewCoins;
        [SerializeField] private RectTransform _tapZoneViewTime;
        [SerializeField] private RectTransform _tapZoneViewLife;

        public void SetPositionTapForOneCard(Vector3 position)
        {
            _tapZoneViewOneCard.position = Camera.main.WorldToScreenPoint(position);
            _tapZoneOneCard.position = Camera.main.WorldToScreenPoint(position);
        }

        public void SetPositionTapForTwoCard(Vector3 position)
        {
            _tapZoneViewTwoCard.position = Camera.main.WorldToScreenPoint(position);
            _tapZoneTwoCard.position = Camera.main.WorldToScreenPoint(position);
        }

        public void SetPositionPointerOnCoins(Vector3 position)
        {
            _tapZoneViewCoins.position = position;
        }
        
        public void SetPositionPointerOnTimer(Vector3 position)
        {
            _tapZoneViewTime.position = position;
        }
        
        public void SetPositionPointerOnLives(Vector3 position)
        {
            _tapZoneViewLife.position = position;
        }
    }
}