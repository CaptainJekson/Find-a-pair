using CJ.FindAPair.Utility;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Installer
{
    public class UICreator : MonoBehaviour
    {
        [SerializeField] private Transform _uiCanvas;

        private UIRoot _uiRoot;
        private LevelMarker _levelMarker;

        [Inject]
        public void ConstructCoreGame(UIRoot uiRoot, LevelMarker levelMarker)
        {
            _uiRoot = uiRoot;
            SetCanvasPosition(_uiRoot.transform);
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