using DG.Tweening;
using UnityEngine;

namespace CJ.FindAPair.Modules.UI.View
{
    public class CloudBlockerForLocation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _animationCloudDuration;

        private string _animatorParametrName = "State";
        
        private enum AnimationState
        {
            Idle,
            OpenFence,
            OpenCloud,
        }

        public void OpenFence()
        {
            _animator.SetInteger(_animatorParametrName,(int)AnimationState.OpenFence);
        }

        public void OpenCloud()
        {
            _animator.SetInteger(_animatorParametrName,(int)AnimationState.OpenCloud);
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