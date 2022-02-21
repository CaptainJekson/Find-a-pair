using CJ.FindAPair.Modules.UI.Tutorial.Base;
using UnityEngine;

namespace CJ.FindAPair.Modules.UI.Tutorial
{
    public class SapperBoosterTutorialScreen : TutorialScreen
    {
        [SerializeField] private RectTransform _tapZoneViewSapperBooster;
        [SerializeField] private RectTransform _tapZoneSapperBooster;
        
        public void SetPositionTapForSapperBooster(Vector3 position)
        {
            _tapZoneViewSapperBooster.position = position;
            _tapZoneSapperBooster.position = position;
        }
    }
}