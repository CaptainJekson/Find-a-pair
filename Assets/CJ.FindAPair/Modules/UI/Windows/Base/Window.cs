using CJ.FindAPair.Modules.Service.Audio;
using Doozy.Engine.UI;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows.Base
{
    [RequireComponent(typeof(UIView))]
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] [CanBeNull] [Tooltip("Can be null")] protected Button _closeButton;

        private UIView _uiView;
        private bool _isOpen;
        private bool _isClose;
        protected AudioController _audioController;
        
        [Inject]
        public void Construct(AudioController audioController)
        {
            _audioController = audioController;
        }

        private void Awake()
        {
            if (_closeButton != null)
                _closeButton.onClick.AddListener(OnCloseButtonClick);

            _uiView = GetComponent<UIView>();
            Init();
        }

        private void OnEnable()
        {
            if (!_isOpen)
            {
                _isOpen = true;
                return;
            }
            
            PlayOpenSound();
            OnOpen();
        }

        private void OnDisable()
        {
            if (!_isClose)
            {
                _isClose = true;
                return;
            }

            OnClose();
        }

        public void Open()
        {
            UIView.ShowView(_uiView.ViewCategory, _uiView.ViewName);
        }

        public void Close()
        {
            UIView.HideView(_uiView.ViewCategory, _uiView.ViewName);
        }

        protected virtual void Init()
        {
        }

        protected virtual void OnOpen()
        {
        }

        protected virtual void OnClose()
        {
        }

        protected virtual void OnCloseButtonClick()
        {
            PlayCloseSound();
            Close();
        }

        protected virtual void PlayOpenSound()
        {
            _audioController.PlaySound(_audioController.AudioClipsCollection.WindowOpenSound);
        }
        
        protected virtual void PlayCloseSound()
        {
            _audioController.PlaySound(_audioController.AudioClipsCollection.WindowCloseSound);
        }
    }
}