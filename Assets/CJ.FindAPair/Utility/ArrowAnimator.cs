using DG.Tweening;
using UnityEngine;

namespace CJ.FindAPair.Utility
{
    public class ArrowAnimator : MonoBehaviour
    {
        [SerializeField] private float _endPositionY = 25.0f;
        
        private Vector3 _startPosition;
        
        private void Start()
        {
            _startPosition = transform.localPosition;
            Play();
        }

        private void Play()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOLocalMoveY(_startPosition.y + _endPositionY, 0.5f).SetEase(Ease.InSine));
            sequence.Append(transform.DOLocalMoveY(_startPosition.y, 0.5f).SetEase(Ease.OutSine));
            sequence.SetLoops(-1, LoopType.Yoyo);
        }
    }
}