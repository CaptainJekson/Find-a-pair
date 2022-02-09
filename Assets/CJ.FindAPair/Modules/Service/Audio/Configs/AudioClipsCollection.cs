using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipsCollection", menuName = "Find a pair/AudioClipsCollection")]
public class AudioClipsCollection : ScriptableObject
{
    [SerializeField] private AudioClip _onLevelMusic;
    [SerializeField] private AudioClip _cardDealSound;
    [SerializeField] private AudioClip _cardFlipSound;
    [SerializeField] private AudioClip _cardDisolveSound;
    [SerializeField] private AudioClip _cardRevealSound;
    [SerializeField] private AudioClip _windowOpenSound;
    [SerializeField] private AudioClip _windowCloseSound;
    [SerializeField] private AudioClip _coinObtainSound;
    [SerializeField] private AudioClip _victorySound;
    [SerializeField] private AudioClip _defeatSound;
    [SerializeField] private AudioClip _giftBoxOpenSound;
    [SerializeField] private AudioClip _giftItemObtainSound;
    [SerializeField] private AudioClip _levelMarkerCenteringSound;
    [SerializeField] private AudioClip _levelMarkerExplosionSound;
    [SerializeField] private AudioClip _detectorBoosterSound;
    [SerializeField] private AudioClip _magnetBoosterSound;
    [SerializeField] private AudioClip _sapperBoosterSound;

    public AudioClip OnLevelMusic => _onLevelMusic;
    public AudioClip CardDealSound => _cardDealSound;
    public AudioClip CardFlipSound => _cardFlipSound;
    public AudioClip CardDisolveSound => _cardDisolveSound;
    public AudioClip CardRevealSound => _cardRevealSound;
    public AudioClip WindowOpenSound => _windowOpenSound;
    public AudioClip WindowCloseSound => _windowCloseSound;
    public AudioClip CoinObtainSound => _coinObtainSound;
    public AudioClip VictorySound => _victorySound;
    public AudioClip DefeatSound => _defeatSound;
    public AudioClip GiftBoxOpenSound => _giftBoxOpenSound;
    public AudioClip GiftItemObtainSound => _giftItemObtainSound;
    public AudioClip LevelMarkerCenteringSound => _levelMarkerCenteringSound;
    public AudioClip LevelMarkerExplosionSound => _levelMarkerExplosionSound;
    public AudioClip DetectorBoosterSound => _detectorBoosterSound;
    public AudioClip MagnetBoosterSound => _magnetBoosterSound;
    public AudioClip SapperBoosterSound => _sapperBoosterSound;
}