using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CJ.FindAPair.Modules.UI
{
    public class LevelMarker : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _explosionEffect;
        [SerializeField] private List<ParticleSystem> _trailEffects;
        [SerializeField] private Transform _centerPosition;
        [SerializeField] private List<Transform> _trailPositions;
        
        //TODO Test
        [SerializeField] private Transform _testTarget;
        [SerializeField] private Transform _testTarget1;
        [SerializeField] private Transform _testTarget2;
        [SerializeField] private Transform _testTarget3;
        [SerializeField] private Transform _testTarget4;

        [Header("Animation settings")]
        [SerializeField] private float _speedSpeed;
        [SerializeField] private float _flyDurationToCenter;
        [SerializeField] private float _flyDurationToTarget;
        [SerializeField] private float _flyDurationFromCenter;

        private bool _isRotate;

        private void Awake()
        {
            _isRotate = true;
            
            //TODO test
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(5.0f);
            sequence.AppendCallback(() => PlayMove(_testTarget));
            sequence.AppendInterval(5.0f);
            sequence.AppendCallback(() => PlayMove(_testTarget1));
            sequence.AppendInterval(5.0f);
            sequence.AppendCallback(() => PlayMove(_testTarget2));
            sequence.AppendInterval(5.0f);
            sequence.AppendCallback(() => PlayMove(_testTarget3));
            sequence.AppendInterval(5.0f);
            sequence.AppendCallback(() => PlayMove(_testTarget4));
            sequence.AppendInterval(5.0f);
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