using DG.Tweening;
using UnityEngine;

public class Transferer
{
    public Sequence TransferItem(GameObject item, Vector3 startPosition, Vector3 endPosition, float moveSpeed, Ease ease = Ease.Linear)
    {
        Sequence transferSequence = DOTween.Sequence();

        return transferSequence
            .Append(item.transform.DOMove(startPosition, 0))
            .Append(item.transform.DOMove(endPosition, moveSpeed).SetEase(ease));
    }
}