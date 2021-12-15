using DG.Tweening;
using UnityEngine;

public class ItemsTransferer
{
    private Sequence _transferSequence;
    
    public void TransferItem(Transform itemTransform, Vector3 startPosition, Vector3 endPosition, float moveDuration, 
        Ease ease = Ease.Linear)
    {
        _transferSequence = DOTween.Sequence();
        
        _transferSequence
            .Append(itemTransform.DOMove(startPosition, 0))
            .AppendCallback(() => itemTransform.gameObject.SetActive(true))
            .Append(itemTransform.DOMove(endPosition, moveDuration).SetEase(ease));
    }
}