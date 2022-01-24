using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioItem : MonoBehaviour
{
    private AudioSource _audioSource;

    public bool AudioSourceIsLoop => _audioSource.loop;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetAudio(AudioClip clip, bool isLoop)
    {
        _audioSource.clip = clip;
        _audioSource.loop = isLoop;
    }

    public void Play()
    {
        gameObject.SetActive(true);
        _audioSource.Play();
        
        if (_audioSource.loop == false)
            DisableOnAudioComplete();
    }

    private void DisableOnAudioComplete()
    {
        Sequence disableSequence = DOTween.Sequence();
        
        disableSequence
            .AppendInterval(_audioSource.clip.length)
            .AppendCallback(() => gameObject.SetActive(false));
    }
}