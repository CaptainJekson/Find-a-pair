using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Transferer
{
    public Sequence TransferItem(GameObject item, Vector3 startPoint, Vector3 endPoint, float transferSpeed, Ease transferEase)
    {
        Sequence transferSequence = DOTween.Sequence();

         return transferSequence
            .Append(item.transform.DOMove(startPoint, 0))
            .Append(item.transform.DOMove(endPoint, transferSpeed).SetEase(transferEase));
    }
}