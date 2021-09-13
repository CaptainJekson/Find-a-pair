using UnityEngine;

namespace CJ.FindAPair.Utility
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaController : MonoBehaviour
    {
        private ScreenOrientation currentOrientation;
        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();

            RefreshSafeArea();
        }

        private void Update()
        {
            if (currentOrientation != Screen.orientation)
            {
                RefreshSafeArea();
            }
        }

        private void RefreshSafeArea()
        {
            var safeArea = Screen.safeArea;
            var anchorMin = safeArea.position;
            var anchorMax = anchorMin + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;

            currentOrientation = Screen.orientation;
        }
    }
}