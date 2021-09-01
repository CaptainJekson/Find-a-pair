using System.Collections;
using CJ.FindAPair.Modules.CoreGames.Configs;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CJ.FindAPair.Modules.CoreGames
{
    [RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(BoxCollider))]
    public class Card : MonoBehaviour
    {
        [SerializeField] private GameSettingsConfig _gameSettingsConfig;
        [SerializeField] private TextMeshProUGUI _textNumber;
        [SerializeField] private Sprite _shirtSprite;
        [SerializeField] private Sprite _faceSprite;
        [SerializeField] private Ease _easeAnimationCard;

        private SpriteRenderer _spriteRenderer;
        private BoxCollider _collider;
        
        public bool IsEmpty { get; set; }
        public bool IsShow { get; set; }
        public bool IsMatched { get; set; }
        public int NumberPair { get; set; }
        
        public event UnityAction СardOpens;
        public event UnityAction CardClosed;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider>();
            IsEmpty = false;
            IsShow = false;
        }
        

        private void Start()
        {
            SetNumberText();

            if (IsEmpty)
            {
                MakeEmpty();
            }

            StartCoroutine(DelayStartHide());
        }
        
        public void SetNumberText()
        {
            _textNumber.SetText(NumberPair.ToString());
        }

        public void Show(bool isNotEventCall = false)
        {
            if(IsEmpty)
                return;
            
            PlayAnimation(true);
            
            IsShow = true;
            if(isNotEventCall) return;
            СardOpens?.Invoke();
        }
        
        public void Hide(bool isNotEventCall = false)
        {
            if(IsEmpty)
                return;
            
            PlayAnimation(false);
            
            IsShow = false;
            IsMatched = false;
            if(isNotEventCall) return;
            CardClosed?.Invoke();
        }
        
        public void DelayHide()
        {
            StartCoroutine(DelayHide(_gameSettingsConfig.DelayTimeHide));
        }
        
        public void MakeEmpty()
        {
            _spriteRenderer.enabled = false;
            _collider.enabled = false;
            _textNumber.enabled = false;
        }

        private IEnumerator DelayHide(float time)
        {
            yield return new WaitForSeconds(time);
            Hide();
        }
        
        private IEnumerator DelayStartHide()
        {
            yield return new WaitForSeconds(_gameSettingsConfig.StartTimeShow);
            Hide();
        }

        private void PlayAnimation(bool isShow)
        {
            var sequence = DOTween.Sequence();

            if(isShow)
                sequence.AppendCallback(() => _collider.enabled = false);
            
            sequence.Append(transform.DORotate(new Vector3(0, 90, 0),
                _gameSettingsConfig.AnimationSpeedCard / 2)).SetEase(_easeAnimationCard);
            sequence.AppendCallback(() =>
            {
                _textNumber.gameObject.SetActive(isShow);
                _spriteRenderer.sprite = isShow ? _faceSprite : _shirtSprite;
            });
            sequence.Append(transform.DORotate(new Vector3(0, 0, 0),
                _gameSettingsConfig.AnimationSpeedCard / 2)).SetEase(_easeAnimationCard);
            
            if(!isShow)
                sequence.AppendCallback(() => _collider.enabled = true);
        }
    }
}