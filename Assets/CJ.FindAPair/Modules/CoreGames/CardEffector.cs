using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CJ.FindAPair.Modules.CoreGames
{
    public class CardEffector : MonoBehaviour
    {
        private const string _SHADER_PROPERTY_NAME = "_Fade";
        
        [SerializeField] private ParticleSystem _electroshockEffect;
        [SerializeField] private ParticleSystem _sapperEffect;
        [SerializeField] private ParticleSystem _magicEyeEffect;
        [SerializeField] private Renderer _renderer;

        [Header("Setting effects")] 
        [SerializeField] private float _dissolveDelay; 
        
        private MaterialPropertyBlock _propBlock;
        private Card _card;

        private void Awake()
        {
            _propBlock = new MaterialPropertyBlock();
            _card = GetComponent<Card>();
        }

        public void PlayDissolve()
        {
            _card.AudioDriver.PlaySound(_card.AudioDriver.AudioClipsCollection.CardDisolveSound, true);
            StartCoroutine(MakeDissolve());
        }

        public void PlayRevertDissolve()
        {
            StartCoroutine(MakeRevertDissolve());
        }

        public void PlayMagicEye()
        {
            _magicEyeEffect.Play();
        }

        public void PlayElectroshock()
        {
            _electroshockEffect.Play();
        }

        public void PlaySapper()
        {
            _sapperEffect.Play();
        }

        private IEnumerator MakeDissolve()
        {
            yield return new WaitForSeconds(_dissolveDelay);    
            
            var currentDissolve = 1.0f;
                
            DOTween.To(()=> currentDissolve, x=> currentDissolve = x, 0.0f, 1);
            
            while (currentDissolve > 0.0f)
            {
                yield return null;
                
                _renderer.GetPropertyBlock(_propBlock);
                _propBlock.SetFloat(_SHADER_PROPERTY_NAME, currentDissolve);
                _renderer.SetPropertyBlock(_propBlock);
            }
        }
        
        private IEnumerator MakeRevertDissolve()
        {
            yield return new WaitForSeconds(_dissolveDelay);
            
            var currentDissolve = 0.0f;
                
            DOTween.To(()=> currentDissolve, x=> currentDissolve = x, 1.0f, 1);
            
            while (currentDissolve < 1.0f)
            {
                yield return null;
                
                _renderer.GetPropertyBlock(_propBlock);
                _propBlock.SetFloat(_SHADER_PROPERTY_NAME, currentDissolve);
                _renderer.SetPropertyBlock(_propBlock);
            }
        }
    }
}