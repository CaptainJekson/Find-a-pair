using CJ.FindAPair.Modules.UI.Tutorial.Base;
using UnityEngine;

namespace CJ.FindAPair.Modules.UI.Tutorial
{
    public class DetectorBoosterTutorialScreen : TutorialScreen
    {
        [SerializeField] private RectTransform _tapZoneViewDetectorBooster;
        [SerializeField] private RectTransform _tapZoneDetectorBooster;
        
        public void SetPositionTapForDetectorBooster(Vector3 position)
        {
            _tapZoneViewDetectorBooster.position = position;
            _tapZoneDetectorBooster.position = position;
        }
    }
}