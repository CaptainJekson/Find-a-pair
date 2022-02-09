using UnityEngine;

public class AudioController: MonoBehaviour
{
    [SerializeField] private AudioClipsCollection _audioClipsCollection;
    [SerializeField] private AudioSource _audioSourcePrefab;

    private AudioSource _musicSource;
    private AudioSource _soundSource;

    public AudioClipsCollection AudioClipsCollection => _audioClipsCollection;

    private void Awake()
    {
        _musicSource = Instantiate(_audioSourcePrefab, transform);
        _soundSource = Instantiate(_audioSourcePrefab, transform);
    }

    public void PlayMusic(AudioClip clip, bool isLoop = true)
    {
        _musicSource.loop = isLoop;
        _musicSource.clip = clip;
        _musicSource.Play();
    }
    
    public void PlaySound(AudioClip clip, bool isSeveral = false)
    {
        if (isSeveral)
        {
            _soundSource.clip = clip;
            _soundSource.Play();
        }
        else
        {
            _soundSource.PlayOneShot(clip);
        }
    }

    public void SetMusicState(bool isMute)
    {
        _musicSource.mute = isMute;
        _musicSource.Play();
    }
    
    public void SetSoundState(bool isMute)
    {
        _soundSource.mute = isMute;
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }
}