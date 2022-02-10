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
            
        }
        
        public void SetPositionPointerOnTimer(Vector3 position)
        {
            
        }
        
        public void SetPositionPointerOnLives(Vector3 position)
        {
            
        }
    }
}