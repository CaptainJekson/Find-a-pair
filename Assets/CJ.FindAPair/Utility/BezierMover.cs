using DG.Tweening;
using UnityEngine;

namespace CJ.FindAPair.Utility
{
    public static class BezierMover
    {
        public static void MoveAlongCurve(this Transform transform, BezierPoint currentPosition,
            BezierPoint targetPosition, float duration, Ease ease = Ease.Linear)
        {
            var delta = 0.0f;
            
            var refreshSequence = DOTween.Sequence();
            refreshSequence.AppendInterval(1.0f / 1000.0f);
            refreshSequence.AppendCallback(() =>
            {
                transform.position = BezierCurve.GetPoint(currentPosition, targetPosition, delta);
            });
            refreshSequence.SetLoops(-1, LoopType.Incremental);

            var sequence = DOTween.Sequence();
            sequence.Append(DOTween.To(() => delta, x => delta = x,
                1.0f, duration).SetEase(ease));
            sequence.AppendCallback(() => refreshSequence.Kill());
        }
    }
}