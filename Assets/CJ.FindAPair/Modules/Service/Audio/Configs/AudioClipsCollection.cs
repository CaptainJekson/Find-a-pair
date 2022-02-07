using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipsCollection", menuName = "Find a pair/AudioClipsCollection")]
public class AudioClipsCollection : ScriptableObject
{
    [SerializeField] private AudioClip _cardDealSound;
    [SerializeField] private AudioClip _cardFlipSound;
    [SerializeField] private AudioClip _cardDisolveSound;
    
    public AudioClip CardDealSound => _cardDealSound;
    public AudioClip CardFlipSound => _cardFlipSound;
    public AudioClip CardDisolveSound => _cardDisolveSound;
}