using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Installer
{
    public class UICreator : MonoBehaviour
    {
        [SerializeField] private Transform _uiCanvas;

        private UIRoot _uiRoot;

        [Inject]
        public void ConstructCoreGame(UIRoot uiRoot)
        {
            _uiRoot = uiRoot;
            SetCanvasPosition(_uiRoot.transform);
        }
        
        private void SetCanvasPosition(Transform transform)
        {
            transform.position = _uiCanvas.position;
            transform.SetParent(_uiCanvas);
            var rectTransform = transform.GetComponent<RectTransform>();
            rectTransform.ResetInZero();
        }
    }
    
    public static class RectTransformExtensions //TODO Move to file Utility class
    {
        public static void SetLeft(this RectTransform rt, float left)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
        }
 
        public static void SetRight(this RectTransform rt, float right)
        {
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
        }
 
        public static void SetTop(this RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
        }
 
        public static void SetBottom(this RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
        }

        public static void ResetInZero(this RectTransform rt)
        {
            rt.SetLeft(0);
            rt.SetRight(0);
            rt.SetTop(0);
            rt.SetBottom(0);
        }
    }
}