using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CJ.FindAPair.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class UIPivotFidex : MonoBehaviour
    {
        [SerializeField] private float _pivotValueX;
        [SerializeField] private float _pivotValueY;

        private RectTransform _rectTransform;

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            StartCoroutine(DelaySetPivot());
        }

        private IEnumerator DelaySetPivot()
        {
            yield return new WaitForSeconds(0.01f);
            _rectTransform.pivot = new Vector2(_pivotValueX, _pivotValueY);
        }
     }
}

