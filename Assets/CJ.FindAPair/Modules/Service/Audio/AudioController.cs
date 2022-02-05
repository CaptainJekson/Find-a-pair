using System.Collections.Generic;
using UnityEngine;

public class AudioController: MonoBehaviour
{
    [SerializeField] private AudioClipsCollection _audioClipsCollection;
    [SerializeField] private AudioSource _audioSourcePrefab;
    [SerializeField] private int _audioSourcesCount;

    private AudioSource _musicSource;
    private List<AudioSource> _audioSources;

    public AudioClipsCollection AudioClipsCollection => _audioClipsCollection;

    private void Awake()
    {
        InitializeAudioSources();
    }

    public void Play(AudioClip clip, bool isMusic, bool isLoop = false)
    {
        var audioItem = _musicSource;
        
        if (isMusic == false)
            audioItem = TryGetAudioSource();

        audioItem.clip = clip;
        audioItem.loop = isLoop;
        audioItem.Play();
    }
    
    public void StopMusic()
    {
        _musicSource.Stop();
    }
    
    public void SwitchAudiosCondition(bool isMusic, bool isMute)
    {
        if (isMusic)
        {
            _musicSource.mute = isMute;
            _musicSource.Play();
        }
        else
        {
            foreach (var source in _audioSources)
            {
                source.mute = isMute;
            }
        }
    }

    private void InitializeAudioSources()
    {
        _audioSources = new List<AudioSource>();

        for (int i = 0; i < _audioSourcesCount; i++)
        {
            _audioSources.Add(Instantiate(_audioSourcePrefab, transform));
        }
        
        _musicSource = Instantiate(_audioSourcePrefab, transform);
    }

    private AudioSource TryGetAudioSource()
    {
        foreach (var item in _audioSources)
        {
            if (item.isPlaying == false)
            {
                return item;
            }
        }

        return null;
    }
}