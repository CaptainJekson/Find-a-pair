using CJ.FindAPair.CardTable;
using UnityEngine;

[RequireComponent(typeof(Card)), RequireComponent(typeof(Animator))]
public class AnimationCard : MonoBehaviour
{
    private Card _card;
    private Animator _animator;

    private void Awake()
    {
        _card = GetComponent<Card>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _card.СardOpens += PlayShowAnimation;
        _card.CardClosed += PlayHideAnimation;
    }

    private void OnDisable()
    {
        _card.СardOpens -= PlayShowAnimation;
        _card.CardClosed -= PlayHideAnimation;
    }

    private void PlayShowAnimation()
    {
        _animator.SetBool("IsHide", false);
    }

    private void PlayHideAnimation()
    {
        _animator.SetBool("IsHide", true);
    }
}
