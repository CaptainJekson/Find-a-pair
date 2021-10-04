using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void ActivateCooldownAnimation(float cooldownTime, TweenCallback makeButtonAvailable)
    {
        Sequence sequence = DOTween.Sequence();

        _image.fillAmount = 1;
        sequence.Append(_image.DOFillAmount(0, cooldownTime));
        sequence.InsertCallback(cooldownTime, makeButtonAvailable);
    }
}