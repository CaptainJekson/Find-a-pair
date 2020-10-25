using CJ.FindAPair.CardTable;
using CJ.FindAPair.Configuration;
using UnityEngine;

namespace CJ.FindAPair.Animation
{
    [RequireComponent(typeof(Card)), RequireComponent(typeof(Animator))]
    public class AnimationCard : MonoBehaviour
    {
        [SerializeField] private GameSettingsConfig _gameSettingConfig;

        private Card _card;
        private Animator _animator;

        private void Awake()
        {
            _card = GetComponent<Card>();
            _animator = GetComponent<Animator>();
            _animator.speed = _gameSettingConfig.AnimationSpeedCard;
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
}
