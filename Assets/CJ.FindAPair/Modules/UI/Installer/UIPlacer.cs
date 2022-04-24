using CJ.FindAPair.Utility;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Installer
{
    public class UIPlacer : MonoBehaviour
    {
        private UIRoot _uiRoot;
        private TutorialRoot _tutorialRoot;

        [Inject]
        public void ConstructCoreGame(UIRoot uiRoot, TutorialRoot tutorialRoot)
        {
            _uiRoot = uiRoot;
            _tutorialRoot = tutorialRoot;
            SetCanvasPosition(_uiRoot.transform);
            SetCanvasPosition(_tutorialRoot.transform);
        }
        
        private void SetCanvasPosition(Transform targetTransform)
        {
            targetTransform.SetParent(transform);
            var rectTransform = targetTransform.GetComponent<RectTransform>();
            rectTransform.ResetInZero();
        }
    }
}