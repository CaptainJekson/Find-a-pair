using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void ActivateCooldownAnimation(float cooldownTime, TweenCallback onCooldownComplete)
    {
        Sequence sequence = DOTween.Sequence();

        _image.fillAmount = 1;
        sequence.Append(_image.DOFillAmount(0, cooldownTime));
        sequence.AppendCallback(onCooldownComplete);
    }
}