using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsForGameOver : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private GameObject startWatchingButton;
    [SerializeField] private bool _testMode = true;           //Не забыть поменять на false!!!! 

    private string _gameId = "3494268";
    private string _myPlacementId = "video";

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(_gameId, _testMode);

        startWatchingButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Advertisement.Show(_myPlacementId);
        });
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (showResult == ShowResult.Skipped)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (showResult == ShowResult.Failed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsDidError(string message)
    {
        
    }
}
