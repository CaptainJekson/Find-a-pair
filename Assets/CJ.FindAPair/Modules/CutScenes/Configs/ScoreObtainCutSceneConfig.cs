using CJ.FindAPair.Modules.CutScenes.Configs.Base;
using DG.Tweening;
using UnityEngine;

namespace CJ.FindAPair.Modules.CutScenes.Configs
{
    [CreateAssetMenu(fileName = "ScoreObtainCutSceneConfig", 
        menuName = "Find a pair/CutScenesConfigs/ScoreObtainCutSceneConfig")]
    public class ScoreObtainCutSceneConfig : CutSceneConfig
    {
        [SerializeField] private ItemsPoolHandler _itemsPoolHandler;
        [SerializeField] private AwardCoin _coinPrefab;
        [SerializeField] private int _temporaryCoinsCount;
        [SerializeField] private float _coinTransferDuration;
        [SerializeField] private Ease _coinTransferEase;

        public ItemsPoolHandler ItemsPoolHandler => _itemsPoolHandler;
        public AwardCoin CoinPrefab => _coinPrefab;
        public int TemporaryCoinsCount => _temporaryCoinsCount; 
        public float CoinTransferDuration => _coinTransferDuration;
        public Ease CoinTransferEase => _coinTransferEase;
    }
}