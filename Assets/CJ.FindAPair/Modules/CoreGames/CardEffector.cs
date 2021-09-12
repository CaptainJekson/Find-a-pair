using UnityEngine;

namespace CJ.FindAPair.Modules.CoreGames
{
    public class CardEffector : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _electroshockEffect;
        [SerializeField] private ParticleSystem _sapperEffect;
        [SerializeField] private ParticleSystem _magicEyeEffect;

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
    }
}