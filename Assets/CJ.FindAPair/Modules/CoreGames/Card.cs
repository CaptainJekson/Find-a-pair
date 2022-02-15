using System;
using System.Collections;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.Service.Audio;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CJ.FindAPair.Modules.CoreGames
{
    [RequireComponent(typeof(BoxCollider)), RequireComponent(typeof(CardEffector))]
    public class Card : MonoBehaviour, IComparable
    {
        [SerializeField] private GameSettingsConfig _gameSettingsConfig;
        [SerializeField] private Sprite _shirtSprite;
        [SerializeField] private Sprite _faceSprite;
        [SerializeField] private SpriteRenderer _visualSprite;
        [SerializeField] private SpriteRenderer _specialCardSprite;
        [SerializeField] private Ease _easeAnimationCard;

        private BoxCollider _collider;
        private bool _isMatched;
        private bool _isDisableTutorial;
        private CardEffector _cardEffector;

        public bool IsEmpty { get; set; }
        public bool IsMatched => _isMatched;
        public int NumberPair { get; set; }
        public AudioController AudioDriver { get; set; }

        public event UnityAction СardOpens;
        public event UnityAction CardClosed;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            _cardEffector = GetComponent<CardEffector>();
            _collider.enabled = false;
            IsEmpty = false;
        }

        private void Start()
        {
            if (IsEmpty)
            {
                MakeEmpty();
            }
            
            PlayAnimation(false);
        }

        public void SetMatchedState()
        {
            _isMatched = true;
            _cardEffector.PlayDissolve();
        }

        public void Show(bool isNotEventCall = false)
        {
            if (IsEmpty)
                return;

            _collider.enabled = false;
            PlayAnimation(true);
            
            if (isNotEventCall) return;
            СardOpens?.Invoke();
        }

        public void Hide(bool isNotEventCall = false)
        {
            if (IsEmpty)
                return;

            if(_isDisableTutorial == false)
                _collider.enabled = true;
            
            PlayAnimation(false);

            if (_isMatched)
            {
                _cardEffector.PlayRevertDissolve();
            }

            _isMatched = false;
            
            if (isNotEventCall) return;
            CardClosed?.Invoke();
        }

        public void DelayHide()
        {
            StartCoroutine(DelayHide(_gameSettingsConfig.DelayTimeHide));
        }

        public void MakeEmpty()
        {
            _visualSprite.enabled = false;
            _collider.enabled = false;
        }

        public void DisableInteractable()
        {
            if(IsEmpty == false)
                _collider.enabled = false;
        }
        
        public void EnableInteractable()
        {
            if (IsEmpty == false && _isDisableTutorial == false)
            {
                _collider.enabled = true;
            }
        }

        public void DisableForTutorial()
        {
            _isDisableTutorial = true;
        }

        public void EnableForTutorial()
        {
            _isDisableTutorial = false;
            EnableInteractable();
        }

        public void SetFace(Sprite face)
        {
            _faceSprite = face;
            _visualSprite.sprite = _faceSprite;
        }

        public void SetSpecialIcon(Sprite specialIcon)
        {
            _specialCardSprite.sprite = specialIcon;
        }

        public void PlayDestroySpecialCard()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(_gameSettingsConfig.AnimationSpeedCard);
            sequence.Append(_specialCardSprite.transform.DOScale(Vector3.zero, 1.0f).SetEase(Ease.InBack));
            sequence.AppendCallback(_cardEffector.PlaySapper);
        }

        public void SetShirt(Sprite shirt)
        {
            _shirtSprite = shirt;
        }

        public int CompareTo(object obj)
        {
            var card = obj as Card;

            if (card != null)
                return NumberPair.CompareTo(card.NumberPair);

            throw new Exception("Unable to compare objects");
        }

        private IEnumerator DelayHide(float time)
        {
            yield return new WaitForSeconds(time);
            Hide();
        }

        public void PlayAnimation(bool isShow)
        {
            var sequence = DOTween.Sequence();
            
            sequence.Append(_visualSprite.transform.DORotate(new Vector3(0, 90, 0),
                _gameSettingsConfig.AnimationSpeedCard / 2)).SetEase(_easeAnimationCard);
            sequence.AppendCallback(() =>
            {
                AudioDriver.PlaySound(AudioDriver.AudioClipsCollection.CardFlipSound, true);
                _specialCardSprite.enabled = isShow;
                _visualSprite.sprite = isShow ? _faceSprite : _shirtSprite;
            });
            sequence.Append(_visualSprite.transform.DORotate(new Vector3(0, 0, 0),
                _gameSettingsConfig.AnimationSpeedCard / 2)).SetEase(_easeAnimationCard);
        }

        public void Move(Vector3 destination, float moveSpeed, Ease moveEase)
        {
            transform.DOMove(destination, moveSpeed).SetEase(moveEase);
        }
    }
}