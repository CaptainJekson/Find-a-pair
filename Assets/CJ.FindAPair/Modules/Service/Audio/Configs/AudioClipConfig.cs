using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipConfig", menuName = "Find a pair/AudioClipConfig")]
public class AudioClipConfig : ScriptableObject
{
    [SerializeField] private AudioClip _mainMenuMusic;
    [SerializeField] private AudioClip _dealCardSound;
    [SerializeField] private AudioClip _cardFlipSound;
    
    public AudioClip MainMenuMusic => _mainMenuMusic;
    public AudioClip DealCardSound => _dealCardSound;
    public AudioClip CardFlipSound => _cardFlipSound;
}