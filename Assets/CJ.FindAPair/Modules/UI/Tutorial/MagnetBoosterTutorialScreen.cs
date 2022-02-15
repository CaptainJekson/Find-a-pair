using CJ.FindAPair.Modules.UI.Tutorial.Base;
using UnityEngine;

namespace CJ.FindAPair.Modules.UI.Tutorial
{
    public class MagnetBoosterTutorialScreen : TutorialScreen
    {
        [SerializeField] private RectTransform _tapZoneViewMagnetBooster;
        [SerializeField] private RectTransform _tapZoneMagnetBooster;
        
        public void SetPositionTapForDetectorBooster(Vector3 position)
        {
            _tapZoneViewMagnetBooster.position = position;
            _tapZoneMagnetBooster.position = position;
        }
    }
}