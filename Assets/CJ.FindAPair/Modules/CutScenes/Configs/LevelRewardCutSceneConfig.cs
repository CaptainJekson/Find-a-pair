using CJ.FindAPair.Modules.CutScenes.Configs.Base;
using DG.Tweening;
using UnityEngine;

namespace CJ.FindAPair.Modules.CutScenes.Configs
{
    [CreateAssetMenu(fileName = "LevelRewardCutSceneConfig", 
        menuName = "Find a pair/CutScenesConfigs/LevelRewardCutSceneConfig")]
    public class LevelRewardCutSceneConfig : CutSceneConfig
    {
        [SerializeField] private ItemsPoolHandler _itemsPoolHandler;
        [SerializeField] private AwardCoin _coinPrefab;
        [SerializeField] private int _maxCoinsOnScene;
        [SerializeField] private float _timeBetweenCoins;
        [SerializeField] private float _delayStartCutScene;
        [SerializeField] private float _coinTransferDuration;
        [SerializeField] private Ease _coinTransferEase;
    
        public ItemsPoolHandler ItemsPoolHandler => _itemsPoolHandler;
        public AwardCoin CoinPrefab => _coinPrefab;
        public int MaxCoinsOnScene => _maxCoinsOnScene;
        public float TimeBetweenCoins => _timeBetweenCoins;
        public float DelayStartCutScene => _delayStartCutScene;
        public float CoinTransferDuration => _coinTransferDuration;
        public Ease CoinTransferEase => _coinTransferEase;
    }
}