using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CutScenes.Configs;
using CJ.FindAPair.Modules.CutScenes.Installer;
using CJ.FindAPair.Modules.CutScenes.CutScenes.Base;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;
using DG.Tweening;
using UnityEngine;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes
{
    public class LevelRewardCutScene : AbstractCutScene
    {
        private GameWatcher _gameWatcher;
        private ISaver _gameSaver;
        private ItemsTransferer _itemsTransferer;
        private LevelRewardCutSceneConfig _cutSceneConfig;
        private GameInterfaceWindow _gameInterfaceWindow;
        private VictoryWindow _victoryWindow;

        private int _rewardCoinsCount;
        private int _currentCoinsCount;
        private int _maxCoinsCount;

        private Sequence _levelRewardSequence;
        private List<AwardCoin> _rewardCoins;

        public LevelRewardCutScene(GameWatcher gameWatcher, ISaver gameSaver, CutScenesConfigs cutScenesConfigs, 
            ItemsTransferer itemsTransferer, UIRoot uiRoot)
        {
            _gameWatcher = gameWatcher;
            _gameSaver = gameSaver;
            _itemsTransferer = itemsTransferer;
            _cutSceneConfig = cutScenesConfigs.GetConfig<LevelRewardCutSceneConfig>();
            _gameInterfaceWindow = uiRoot.GetWindow<GameInterfaceWindow>();
            _victoryWindow = uiRoot.GetWindow<VictoryWindow>();
        }

        public override void Play()
        {
            _levelRewardSequence = DOTween.Sequence();

            _maxCoinsCount = _gameWatcher.Score;
            _rewardCoinsCount = _gameWatcher.Score;
            _currentCoinsCount = _gameSaver.LoadData().ItemsData.Coins;

            CheckReceivedScores();
            InitializeItemsPool(_cutSceneConfig.ItemsPoolHandler, _cutSceneConfig.CoinPrefab.gameObject, 
                _victoryWindow.CoinsParentTransform, _maxCoinsCount);

            _victoryWindow.SetCoinsValue(_currentCoinsCount);

            _levelRewardSequence
                .AppendInterval(_cutSceneConfig.DelayStartCutScene);

            for (int i = 0; i < _maxCoinsCount; i++)
            {
                int coinsValue = ++_currentCoinsCount;
                int coinNumber = i;
                int decreasedScoresValue = _rewardCoinsCount - i;

                _levelRewardSequence
                    .AppendCallback(() =>
                        _gameInterfaceWindow.DecreaseScores(decreasedScoresValue, --decreasedScoresValue, 0))
                    .AppendCallback(() => TransferCoin(coinNumber, coinsValue))
                    .AppendInterval(_cutSceneConfig.TimeBetweenCoins);
            }
        }

        public override void Stop()
        {
            if (_victoryWindow.gameObject.activeSelf)
            {
                _levelRewardSequence.Kill();
                _gameWatcher.ResetScore();
                _cutSceneConfig.ItemsPoolHandler.DestroyItemsPool(ItemsPool);
                _rewardCoins.Clear();
            }
        }

        protected override void InitializeItemsPool(ItemsPoolHandler itemsPoolHandler, GameObject item, 
            Transform creationTransform, int itemsCount)
        {
            base.InitializeItemsPool(itemsPoolHandler, item, creationTransform, itemsCount);
        
            _rewardCoins = new List<AwardCoin>();
        
            foreach (var coinPrefabClone in ItemsPool)
                _rewardCoins.Add(coinPrefabClone.GetComponent<AwardCoin>());
        }
    
        private void CheckReceivedScores()
        {
            if (_gameWatcher.Score > _cutSceneConfig.MaxCoinsOnScene)
            {
                _maxCoinsCount = _cutSceneConfig.MaxCoinsOnScene;
                _rewardCoinsCount -= _gameWatcher.Score - _cutSceneConfig.MaxCoinsOnScene;
                _currentCoinsCount += _gameWatcher.Score - _cutSceneConfig.MaxCoinsOnScene;
            }
        }

        private void TransferCoin(int coinNumber, int coinsValue)
        {
            Sequence transferSequence = DOTween.Sequence();
        
            transferSequence
                .AppendCallback(() => _itemsTransferer.TransferItem(_rewardCoins[coinNumber].transform, 
                    _gameInterfaceWindow.ScoresIconTransform.position, _victoryWindow.CoinsParentTransform.position, 
                    _cutSceneConfig.CoinTransferDuration, _cutSceneConfig.CoinTransferEase))
                .AppendInterval(_cutSceneConfig.CoinTransferDuration)
                .AppendCallback(() =>
                {
                    _rewardCoins[coinNumber].gameObject.SetActive(false);
                    _victoryWindow.SetCoinsValue(coinsValue);
                });
        }
    }
}