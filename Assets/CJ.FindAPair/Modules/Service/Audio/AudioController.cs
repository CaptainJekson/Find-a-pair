using System.Collections.Generic;
using CJ.FindAPair.Modules.Meta.Configs;
using UnityEngine;
using Zenject;

public class AudioController: MonoBehaviour
{
    [SerializeField] private ItemsPoolHandler _itemsPoolHandler;
    [SerializeField] private AudioClipsCollection _audioClipsCollection;
    [SerializeField] private AudioItem _audioItem;
    [SerializeField] private int _audioSourcesCount;

    private AudioItem _musicItem;
    private List<AudioItem> _audioItemsPool;

    public AudioClipsCollection AudioClipsCollection => _audioClipsCollection;

    private void Awake()
    {
        InitializeAudioItemsPool();
        InitializeMusicItem();
    }

    public void ActivateAudio(AudioClip clip, bool isMusic, bool isLoop = false)
    {
        var audioItem = _musicItem;
        
        if (isMusic == false)
            audioItem = TryGetAudio();

        audioItem.SetAudio(clip, isLoop);
        audioItem.Play();
    }

    public void DisableMusic()
    {
        _musicItem.gameObject.SetActive(false);
    }

    private void InitializeAudioItemsPool()
    {
        _audioItemsPool = new List<AudioItem>();
        
        var audioSourcesPool = _itemsPoolHandler.GetItemsPool(_audioItem.gameObject, transform, 
            _audioSourcesCount);

        foreach (var source in audioSourcesPool)
        {
            _audioItemsPool.Add(source.GetComponent<AudioItem>());
        }
    }

    private void InitializeMusicItem()
    {
        _musicItem = Instantiate(_audioItem, transform);
        _musicItem.gameObject.SetActive(false);
    }
    
    private AudioItem TryGetAudio()
    {
        foreach (var item in _audioItemsPool)
        {
            if (item.gameObject.activeSelf == false)
            {
                return item;
            }
        }

        return null;
    }
}