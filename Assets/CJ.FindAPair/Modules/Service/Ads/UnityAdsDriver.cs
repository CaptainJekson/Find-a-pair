using System;
using CJ.FindAPair.Modules.Service.Ads.Configs;
using UnityEngine.Advertisements;

namespace CJ.FindAPair.Modules.Service.Ads
{
    public class UnityAdsDriver : IUnityAdsListener, IAdsDriver 
    {
        private UnityAdsConfig _unityAdsConfig;

        public UnityAdsDriver(UnityAdsConfig unityAdsConfig)
        {
            _unityAdsConfig = unityAdsConfig;
            
            Advertisement.AddListener(this);
            Advertisement.Initialize(_unityAdsConfig.GameId, _unityAdsConfig.TestMode);
        }

        public event Action<string> AdsIsReady;
        public event Action<string> AdsIsStarted;
        public event Action<string> AdsIsComplete;
        public event Action<string> AdsIsSkipped;
        public event Action<string> AdsIsFailed;
        public event Action<string> AdsError;

        public void ShowAds(string placementId)
        {
            Advertisement.Show(placementId);
        }
        
        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            switch (showResult)
            {
                case ShowResult.Finished:
                    AdsIsComplete?.Invoke(placementId);
                    break;
                case ShowResult.Skipped:
                    AdsIsSkipped?.Invoke(placementId);
                    break;
                case ShowResult.Failed:
                    AdsIsFailed?.Invoke(placementId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(showResult), showResult, null);
            }
        }
        
        public void OnUnityAdsReady(string placementId)
        {
            AdsIsReady?.Invoke(placementId);
        }

        public void OnUnityAdsDidError(string message) 
        {
            AdsError?.Invoke(message);
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            AdsIsStarted?.Invoke(placementId);
        }
    }
}