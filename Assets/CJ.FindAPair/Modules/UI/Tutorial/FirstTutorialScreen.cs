using CJ.FindAPair.Modules.UI.Tutorial.Base;
using UnityEngine;

namespace CJ.FindAPair.Modules.UI.Tutorial
{
    public class FirstTutorialScreen : TutorialScreen
    {
        [SerializeField] private RectTransform _tapZoneViewOneCard;
        [SerializeField] private RectTransform _tapZoneOneCard;

        public void SetPositionTapForOneCard(Vector3 position)
        {
            _tapZoneViewOneCard.position = Camera.main.WorldToScreenPoint(position);;
            _tapZoneOneCard.position = Camera.main.WorldToScreenPoint(position);;
        }
    }
}