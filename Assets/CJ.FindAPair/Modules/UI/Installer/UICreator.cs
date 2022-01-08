using CJ.FindAPair.Utility;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Installer
{
    public class UICreator : MonoBehaviour
    {
        [SerializeField] private Transform _uiCanvas;

        private UIRoot _uiRoot;
        private TutorialRoot _tutorialRoot;
        private LevelMarker _levelMarker;

        [Inject]
        public void ConstructCoreGame(UIRoot uiRoot, TutorialRoot tutorialRoot, LevelMarker levelMarker)
        {
            _uiRoot = uiRoot;
            _tutorialRoot = tutorialRoot;
            SetCanvasPosition(_uiRoot.transform);
            SetCanvasPosition(_tutorialRoot.transform);
            _levelMarker = levelMarker;
        }
        
        private void SetCanvasPosition(Transform transform)
        {
            transform.position = _uiCanvas.position;
            transform.SetParent(_uiCanvas);
            var rectTransform = transform.GetComponent<RectTransform>();
            rectTransform.ResetInZero();
        }
    }
}