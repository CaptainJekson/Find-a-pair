using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.UI
{
    [RequireComponent(typeof(Button))]
    class UICloseCurrenScreenButton : MonoBehaviour
    {
        [SerializeField] private UIView _currentView;
        private Button _closeButton;

        private void Awake()
        {
            _closeButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseScreen);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseScreen);
        }

        private void CloseScreen()
        {
            UIView.HideView(_currentView.ViewCategory, _currentView.ViewName);
        }
    }
}
