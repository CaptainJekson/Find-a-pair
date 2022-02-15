using CJ.FindAPair.Modules.UI.Tutorial.Base;
using UnityEngine;

namespace CJ.FindAPair.Modules.UI.Tutorial
{
    public class FortuneCardTutorialScreen : TutorialScreen
    {
        [SerializeField] private RectTransform _tapZoneViewFortuneCard;
        [SerializeField] private RectTransform _tapZoneFortuneCard;
        
        public void SetPositionTapForFortuneCard(Vector3 position)
        {
            _tapZoneViewFortuneCard.position = Camera.main.WorldToScreenPoint(position);
            _tapZoneFortuneCard.position = Camera.main.WorldToScreenPoint(position);
        }
    }
}