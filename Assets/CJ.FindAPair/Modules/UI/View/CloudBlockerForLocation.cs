using DG.Tweening;
using UnityEngine;

namespace CJ.FindAPair.Modules.UI.View
{
    public class CloudBlockerForLocation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _animationCloudDuration;

        private string _animatorParametrName = "IsOpen";

        public void Unlock()
        {
            _animator.SetBool(_animatorParametrName, true);
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(_animationCloudDuration);
            sequence.AppendCallback(() => gameObject.SetActive(false));
        }

        public void UnlockFast()
        {
            gameObject.SetActive(false);
        }
    }
}