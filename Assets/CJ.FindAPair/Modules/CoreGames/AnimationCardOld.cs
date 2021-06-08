using System.Collections;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CJ.FindAPair.Animation
{
    //[RequireComponent(typeof(CardOld)), RequireComponent(typeof(Animator))]
    public class AnimationCardOld : MonoBehaviour
    {
        // [SerializeField] private GameSettingsConfig _gameSettingConfig;
        // [SerializeField] private TextMeshProUGUI _comboValueText;
        // [SerializeField] private Transform _pointEndCombo;
        //
        // [SerializeField] private ParticleSystem _comboEffect;
        // [SerializeField] private ParticleSystem _magicEyeEffect;
        // [SerializeField] private ParticleSystem _electroshockEffect;
        // [SerializeField] private ParticleSystem _sapperEffect;
        //
        // [Header("Dotween settings")] 
        // [SerializeField] private float _durationComboText;
        // [SerializeField] private Ease _easeComboText;
        //
        // private CardOld _cardOld;
        // private Animator _animator;
        // private float _startSizeCard;
        //
        // private void Awake()
        // {
        //     _cardOld = GetComponent<CardOld>();
        //     _animator = GetComponent<Animator>();
        //     _animator.speed = _gameSettingConfig.AnimationSpeedCard;
        // }
        //
        // private void Start()
        // {
        //     _startSizeCard = gameObject.GetComponent<RectTransform>().sizeDelta.x;
        //     StartCoroutine(ChangeScaleParticle());
        // }
        //
        // private void OnEnable()
        // {
        //     _cardOld.CardOpensForAnimation += PlayShowAnimation;
        //     _cardOld.CardClosedForAnimation += PlayHideAnimation;
        // }
        //
        // private void OnDisable()
        // {
        //     _cardOld.CardOpensForAnimation -= PlayShowAnimation;
        //     _cardOld.CardClosedForAnimation -= PlayHideAnimation;
        // }
        //
        // public void PlayMagicEye()
        // {
        //     _magicEyeEffect.Play();
        // }
        //
        // public void PlayElectroshock()
        // {
        //     _electroshockEffect.Play();
        // }
        //
        // public void PlaySapper()
        // {
        //     _sapperEffect.Play();
        // }
        //
        // public void PlayCombo(int scoreCombo)
        // {
        //     _comboEffect.Play();
        //     
        //     var sequence = DOTween.Sequence();
        //     sequence.AppendCallback(() => _comboValueText.gameObject.SetActive(true));
        //     sequence.AppendCallback(() => _comboValueText.SetText($"+{(scoreCombo / 2).ToString()}"));
        //     sequence.Append(_comboValueText.transform.DOMove(_pointEndCombo.position, _durationComboText)
        //         .SetEase(_easeComboText));
        //     sequence.AppendCallback(() => _comboValueText.gameObject.SetActive(false));
        // }
        //
        // private void PlayShowAnimation()
        // {
        //     _animator.SetBool("IsHide", false);
        // }
        //
        // private void PlayHideAnimation()
        // {
        //     _animator.SetBool("IsHide", true);
        // }
        //
        // private IEnumerator ChangeScaleParticle()
        // {
        //     yield return new WaitForSeconds(0.2f);
        //     var newScale = gameObject.GetComponent<RectTransform>().sizeDelta.x / _startSizeCard;
        //     
        //     _comboEffect.transform.localScale = Vector3.one * newScale; 
        //     _magicEyeEffect.transform.localScale = Vector3.one * newScale; 
        //     _electroshockEffect.transform.localScale = Vector3.one * newScale; 
        //     _sapperEffect.transform.localScale = Vector3.one * newScale; 
        // }
    }
}
