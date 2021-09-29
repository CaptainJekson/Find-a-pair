using System;

namespace CJ.FindAPair.Modules.Service.Ads
{
    public interface IAdsDriver
    {
        event Action<string> AdsIsReady;
        event Action<string> AdsIsStarted;
        event Action<string> AdsIsComplete;
        event Action<string> AdsIsSkipped;
        event Action<string> AdsIsFailed;
        event Action<string> AdsError;
        void ShowAds(string placementId);
    }
}