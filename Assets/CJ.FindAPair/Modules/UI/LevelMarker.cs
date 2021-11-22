using System.Collections.Generic;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.UI
{
    public class LevelMarker : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _explosionEffect;
        [SerializeField] private List<ParticleSystem> _trailEffects;
        [SerializeField] private Transform _centerPosition;
        [SerializeField] private List<Transform> _trailPositions;

        [Header("Animation settings")]
        [SerializeField] private float _speedSpeed;
        [SerializeField] private float _flyDurationToCenter;
        [SerializeField] private float _flyDurationToTarget;
        [SerializeField] private float _flyDurationFromCenter;

        private bool _isRotate;
        private LevelMapWindow _levelMapWindow;

        [Inject]
        public void Construct(UIRoot uiRoot)
        {
            _isRotate = true;
            _levelMapWindow = uiRoot.GetWindow<LevelMapWindow>();
            
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(1.0f);
            sequence.AppendCallback(Init);
        }

        private void Init()
        {
            var currentLocationAndButton = _levelMapWindow.GetCurrentLocationAndButton();
            transform.SetParent(currentLocationAndButton.Key.transform, false);
            transform.position = currentLocationAndButton.Value.transform.position;
        }
            
        private void Update()
        {
            if(_isRotate)
                transform.Rotate(0,0, _speedSpeed);
        }
        
        private void PlayMove(Transform target)
        {
            _isRotate = false;
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() =>
            {
                foreach (var trail in _trailEffects)
                {
                    trail.transform.DOMove(_centerPosition.position, _flyDurationToCenter);
                }
            });
            sequence.AppendInterval(_flyDurationToCenter);
            sequence.Append(transform.DOMove(target.position, _flyDurationToTarget));
            sequence.AppendCallback(() =>
            {
                _explosionEffect.Play();
                
                for (var i = 0; i < _trailEffects.Count; i++)
                {
                    _trailEffects[i].transform.DOMove(_trailPositions[i].position, _flyDurationFromCenter);
                }
            });
            sequence.AppendInterval(_flyDurationFromCenter);
            sequence.AppendCallback(() => _isRotate = true);
        }
    }
}