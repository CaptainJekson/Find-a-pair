using DG.Tweening;
using UnityEngine;

public class Transferer
{
    public void TransferItem(Transform itemTransform, Vector3 startPosition, Vector3 endPosition, float moveSpeed, Ease ease = Ease.Linear)
    {
        Sequence transferSequence = DOTween.Sequence();
        
        transferSequence
            .Append(itemTransform.transform.DOMove(startPosition, 0))
            .Append(itemTransform.transform.DOMove(endPosition, moveSpeed).SetEase(ease));
    }
}