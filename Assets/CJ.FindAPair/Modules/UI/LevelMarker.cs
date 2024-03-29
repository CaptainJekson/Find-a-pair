﻿using System;
using System.Collections.Generic;
using CJ.FindAPair.Modules.Service.Audio;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;
using CJ.FindAPair.Utility;
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
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _flyDurationToCenter;
        [SerializeField] private float _flyDurationToTarget;
        [SerializeField] private float _flyDurationFromCenter;

        private bool _isRotate;
        private LevelMapWindow _levelMapWindow;
        private AudioController _audioController;
        private BezierPoint _currentBezierPoint;

        [Inject]
        public void Construct(UIRoot uiRoot, AudioController audioController)
        {
            _isRotate = true;
            _levelMapWindow = uiRoot.GetWindow<LevelMapWindow>();
            _audioController = audioController;
            
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(1.0f);
            sequence.AppendCallback(Init);
        }

        private void Init()
        {
            var currentLocationAndButton = _levelMapWindow.GetCurrentLocationAndButton();
            transform.SetParent(currentLocationAndButton.Key.transform, false);
            _currentBezierPoint = currentLocationAndButton.Value.BezierPoint;
            transform.position = _currentBezierPoint.transform.position;
        }
            
        private void Update()
        {
            if (_isRotate)
            {
                transform.Rotate(0,0, _rotateSpeed);
            }
        }

        public void MoveToNextLevelButton(Action explosionOccurred = null, Action moveComplete = null)
        {
            _isRotate = false;
            
            var currentLevelLocationAndButton = _levelMapWindow.GetCurrentLocationAndButton();
            var targetPoint = currentLevelLocationAndButton.Value.BezierPoint;
            
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() =>
            {
                _audioController.PlaySound(_audioController.AudioClipsCollection.LevelMarkerCenteringSound);
                
                foreach (var trail in _trailEffects)
                {
                    trail.transform.DOMove(_centerPosition.position, _flyDurationToCenter);
                }
            });
            sequence.AppendInterval(_flyDurationToCenter);
            sequence.AppendCallback(() =>
            {
                transform.MoveAlongCurve(_currentBezierPoint, targetPoint, _flyDurationToTarget, Ease.InOutSine);
                _currentBezierPoint = targetPoint;
            });
            sequence.AppendInterval(_flyDurationToTarget); 
            sequence.AppendCallback(() =>
            {
                _explosionEffect.Play();
                _audioController.PlaySound(_audioController.AudioClipsCollection.LevelMarkerExplosionSound);
                explosionOccurred?.Invoke();
                
                for (var i = 0; i < _trailEffects.Count; i++)
                {
                    _trailEffects[i].transform.DOMove(_trailPositions[i].position, _flyDurationFromCenter);
                }
            });
            sequence.AppendInterval(_flyDurationFromCenter);
            sequence.AppendCallback(() =>
            {
                _isRotate = true;
                transform.SetParent(currentLevelLocationAndButton.Key.transform, true);
                moveComplete?.Invoke();
            });
        }
    }
}