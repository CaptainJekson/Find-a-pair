using System.Collections;
using CJ.FindAPair.CardTable;
using CJ.FindAPair.Configuration;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CJ.FindAPair.Animation
{
    [RequireComponent(typeof(Card)), RequireComponent(typeof(Animator))]
    public class AnimationCard : MonoBehaviour
    {
        [SerializeField] private GameSettingsConfig _gameSettingConfig;
        [SerializeField] private TextMeshProUGUI _comboValueText;
        [SerializeField] private Transform _pointEndCombo;
        
        [SerializeField] private ParticleSystem _comboEffect;

        [Header("Dotween settings")] 
        [SerializeField] private float _durationComboText;
        [SerializeField] private Ease _easeComboText;
        
        private Card _card;
        private Animator _animator;
        private float _startSizeCard;

        private void Awake()
        {
            _card = GetComponent<Card>();
            _animator = GetComponent<Animator>();
            _animator.speed = _gameSettingConfig.AnimationSpeedCard;
        }

        private void Start()
        {
            _startSizeCard = gameObject.GetComponent<RectTransform>().sizeDelta.x;
            StartCoroutine(ChangeScaleParticle());
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
        
        public void PlayCombo(int scoreCombo)
        {
            _comboEffect.Play();
            
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => _comboValueText.gameObject.SetActive(true));
            sequence.AppendCallback(() => _comboValueText.SetText($"+{(scoreCombo / 2).ToString()}"));
            sequence.Append(_comboValueText.transform.DOMove(_pointEndCombo.position, _durationComboText)
                .SetEase(_easeComboText));
            sequence.AppendCallback(() => _comboValueText.gameObject.SetActive(false));
        }

        private void PlayShowAnimation()
        {
            _animator.SetBool("IsHide", false);
        }

        private void PlayHideAnimation()
        {
            _animator.SetBool("IsHide", true);
        }
        
        private IEnumerator ChangeScaleParticle()
        {
            yield return new WaitForSeconds(0.2f);
            var newScale = gameObject.GetComponent<RectTransform>().sizeDelta.x / _startSizeCard;
            
            _comboEffect.transform.localScale = Vector3.one * newScale; 
        }
    }
}
