using CJ.FindAPair.Modules.UI.Tutorial.Base;
using UnityEngine;

namespace CJ.FindAPair.Modules.UI.Tutorial
{
    public class EntanglementCardTutorialScreen : TutorialScreen
    {
        [SerializeField] private RectTransform _tapZoneViewEntanglementCard;
        [SerializeField] private RectTransform _tapZoneEntanglementCard;
        
        public void SetPositionTapForEntanglementCard(Vector3 position)
        {
            _tapZoneViewEntanglementCard.position = Camera.main.WorldToScreenPoint(position);
            _tapZoneEntanglementCard.position = Camera.main.WorldToScreenPoint(position);
        }
    }
}