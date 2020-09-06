using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Animator))]
public class Card : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberCard;
    [SerializeField] private ParticleSystem _guessedCardEffect;
    [SerializeField] private float _startTimeShow;
    [SerializeField] private float _timeShow;

    private Button _button;
    private Animator _animator;
    private CardComparer _cardComparer;
    private AudioSource _soundsShowCard;
    private AudioSource _soundGuessingCards;

    public bool IsGuessed { get; private set; }
    public int Number
    {
        get => Convert.ToInt32(_numberCard.text);
        set => _numberCard.text = value.ToString();
    }

    private CardState State 
    {
        get => (CardState)_animator.GetInteger("State");
        set => _animator.SetInteger("State", (int)value);
    }

    private enum CardState
    {
        Idle, ShowCard, HideCard
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(DelayHideAnimation(_startTimeShow));
    }

    public void OnCardClick()
    {
        ShowCard();

        _cardComparer.AddCardToCompare(this);

        if (_cardComparer.IsCoupleCard)
            _cardComparer.ToCompare();
    }

    public void Init(CardComparer cardComparer, AudioSource soundsShowCard, AudioSource soundGuessingCards)
    {
        _cardComparer = cardComparer;
        _soundsShowCard = soundsShowCard;
        _soundGuessingCards = soundGuessingCards;
    }

    public void RemoveCard()
    {
        Destroy(gameObject);
    }

    public void HideCard()
    {
        _button.enabled = true;
        State = CardState.ShowCard;
        StartCoroutine(DelayHideAnimation(_timeShow));
    }

    public void ActivateGuessing()
    {
        _guessedCardEffect.Play();
        _soundGuessingCards.Play();
        IsGuessed = true;
    }

    private void ShowCard()
    {
        _button.enabled = false;
        _soundsShowCard.Play();
        State = CardState.ShowCard;
    }

    private IEnumerator DelayHideAnimation(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        State = CardState.HideCard;
    }
}

