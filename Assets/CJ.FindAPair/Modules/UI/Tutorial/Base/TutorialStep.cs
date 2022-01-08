using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.Modules.UI.Tutorial.Base
{
    public class TutorialStep : MonoBehaviour
    {
        [SerializeField] private Button _nextStepButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private bool _isTimedStep;
        [SerializeField] private float _timeNextStep;
        [SerializeField] private float _delayShow;
        [SerializeField] private float _fadeShowDuration;
        [SerializeField] private float _fadeHideDuration;
  
        private Action _action;
        private TutorialScreen _tutorialScreen;
        
        private void Awake()
        {
            if (_isTimedStep)
            {
                var sequence = DOTween.Sequence();
                sequence.AppendInterval(_timeNextStep);
                sequence.AppendCallback(_tutorialScreen.ShowNextStep);
            }
            else
            {
                _nextStepButton.onClick.AddListener(OnButtonClick);
            }
        }

        public void Init(TutorialScreen tutorialScreen)
        {
            _tutorialScreen = tutorialScreen;
        }

        public void Show()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(_delayShow);
            sequence.AppendCallback(() => gameObject.SetActive(true));
            sequence.Append(_canvasGroup.DOFade(1.0f, _fadeShowDuration).From(0.0f));
        }
        
        public void Hide()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(_delayShow);
            sequence.Append(_canvasGroup.DOFade(0.0f, _fadeHideDuration).From(1.0f));
            sequence.AppendCallback(() => gameObject.SetActive(false));
        }

        public void SetAction(Action action)
        {
            _action = action;
        }

        private void OnButtonClick()
        {
            _tutorialScreen.ShowNextStep();
            _action?.Invoke();
            _action = null;
        }
    }
}
