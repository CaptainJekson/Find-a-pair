using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioItem : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void SetAudio(AudioClip clip, bool isLoop)
    {
        _audioSource.clip = clip;
        _audioSource.loop = isLoop;
    }

    public void Play()
    {
        gameObject.SetActive(true);
        
        if (_audioSource.loop == false)
            DisableOnClipComplete();
    }

    private void DisableOnClipComplete()
    {
        Sequence disableSequence = DOTween.Sequence();
        
        disableSequence
            .AppendInterval(_audioSource.clip.length)
            .AppendCallback(() => gameObject.SetActive(false));
    }
}