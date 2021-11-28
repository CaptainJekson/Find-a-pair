using System;
using System.Collections;
using CJ.FindAPair.Modules.CoreGames.Configs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CJ.FindAPair.Modules.CoreGames
{
    [RequireComponent(typeof(BoxCollider))]
    public class Card : MonoBehaviour, IComparable
    {
        [SerializeField] private GameSettingsConfig _gameSettingsConfig;
        [SerializeField] private Sprite _shirtSprite;
        [SerializeField] private Sprite _faceSprite;
        [SerializeField] private SpriteRenderer _visualSprite;
        [SerializeField] private SpriteRenderer _specialCardSprite;
        [SerializeField] private Ease _easeAnimationCard;

        private BoxCollider _collider;

        public bool IsEmpty { get; set; }
        public bool IsShow { get; set; }
        public bool IsMatched { get; set; }
        public int NumberPair { get; set; }
        
        public event UnityAction СardOpens;
        public event UnityAction CardClosed;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            _collider.enabled = false;
            IsEmpty = false;
            IsShow = false;
        }

        private void Start()
        {
            if (IsEmpty)
            {
                MakeEmpty();
            }
            
            PlayAnimation(false);
        }

        public void Show(bool isNotEventCall = false)
        {
            if (IsEmpty)
                return;

            _collider.enabled = false;
            PlayAnimation(true);
            
            IsShow = true;
            if (isNotEventCall) return;
            СardOpens?.Invoke();
        }

        public void Hide(bool isNotEventCall = false)
        {
            if (IsEmpty)
                return;

            _collider.enabled = true;
            PlayAnimation(false);
            
            IsShow = false;
            IsMatched = false;
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
            if(IsEmpty == false)
                _collider.enabled = true;
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