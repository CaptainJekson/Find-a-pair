using System.Collections.Generic;
using UnityEngine;

public class AudioController: MonoBehaviour
{
    [SerializeField] private ItemsPoolHandler _itemsPoolHandler;
    [SerializeField] private AudioClipConfig _audioClipConfig;
    [SerializeField] private AudioItem _audioItem;
    [SerializeField] private int _audioSourcesCount;

    private List<AudioItem> _audioItemsPool;

    public AudioClipConfig AudioClipConfig => _audioClipConfig;

    private void Awake()
    {
        InitializeAudioItemsPool();
    }

    public void ActivateAudio(AudioClip clip, bool isLoop = false)
    {
        var audioItem = TryGetAudio();
        
        audioItem.SetAudio(clip, isLoop);
        audioItem.Play();
    }

    public void DisableLoopedAudios()
    {
        foreach (var item in _audioItemsPool)
        {
            if (item.gameObject.activeSelf && item.AudioSourceIsLoop)
            {
                item.gameObject.SetActive(false);
            }
        }
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