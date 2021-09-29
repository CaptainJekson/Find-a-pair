using UnityEngine;

namespace CJ.FindAPair.Modules.Service.Ads.Configs
{
    [CreateAssetMenu(fileName = "UnityAdsConfig", menuName = "Find a pair/Service/AdsConfig")]
    public class UnityAdsConfig : ScriptableObject
    {
        [SerializeField] private bool _testMode;
        [SerializeField] private string _gameId;
        [SerializeField] private string _PlacementVideoId;
        [SerializeField] private string _PlacementRewardedVideoId;
        
        public bool TestMode => _testMode;
        public string GameId => _gameId;
        public string PlacementVideoId => _PlacementVideoId;
        public string PlacementRewardedVideoId => _PlacementRewardedVideoId;
    }
}