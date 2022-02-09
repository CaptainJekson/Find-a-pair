using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CutScenes.Configs;
using CJ.FindAPair.Modules.CutScenes.CutScenes.Base;
using CJ.FindAPair.Modules.CutScenes.Installer;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;
using DG.Tweening;
using UnityEngine;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes
{
    public class ScoreObtainCutScene : AbstractCutScene
    {
        private GameWatcher _gameWatcher;
        private ItemsTransferer _itemsTransferer;
        private CardComparator _cardComparator;
        private ScoreObtainCutSceneConfig _cutSceneConfig;
        private ComboValueCutScene _comboValueCutScene;
        private GameInterfaceWindow _gameInterfaceWindow;
        private Camera _camera;
        private AudioController _audioController;

        private Sequence _scoreObtainSequence;
        private List<AwardCoin> _temporaryCoins;
    
        public ScoreObtainCutScene(GameWatcher gameWatcher, ItemsTransferer itemsTransferer, UIRoot uiRoot, 
            CardComparator cardComparator, CutScenesConfigs cutScenesConfigs, ComboValueCutScene comboValueCutScene, 
            AudioController audioController)
        {
            _gameWatcher = gameWatcher;
            _itemsTransferer = itemsTransferer;
            _cardComparator = cardComparator;
            _cutSceneConfig = cutScenesConfigs.GetConfig<ScoreObtainCutSceneConfig>();
            _comboValueCutScene = comboValueCutScene;
            _gameInterfaceWindow = uiRoot.GetWindow<GameInterfaceWindow>();
            _camera = Camera.main;
            _audioController = audioController;
        }
    
        public override void Play()
        {
            _scoreObtainSequence = DOTween.Sequence();

            var coinStartPosition = _camera.WorldToScreenPoint(_cardComparator
                .ComparisonCards[_cardComparator.ComparisonCards.Count - 1].transform.position);

            foreach (var availableCoin in _temporaryCoins)
            {
                if (availableCoin.gameObject.activeSelf == false)
                {
                    if (_gameWatcher.ComboCounter > 1)
                        _comboValueCutScene.Play();
                
                    _scoreObtainSequence
                        .AppendCallback(() => _itemsTransferer.TransferItem(availableCoin.transform, 
                            coinStartPosition, _gameInterfaceWindow.ScoresIconTransform.position, 
                            _cutSceneConfig.CoinTransferDuration, _cutSceneConfig.CoinTransferEase))
                        .AppendInterval(_cutSceneConfig.CoinTransferDuration)
                        .AppendCallback(() =>
                        {
                            _audioController.PlaySound(_audioController.AudioClipsCollection.CoinObtainSound);
                            availableCoin.gameObject.SetActive(false);
                        });

                    break;
                }
            }
        }

        public override void Stop()
        {
            _scoreObtainSequence.Kill();
            _cutSceneConfig.ItemsPoolHandler.DestroyItemsPool(ItemsPool);
            _temporaryCoins.Clear();
        
            _comboValueCutScene.Stop();
        }

        protected override void InitializeItemsPool(ItemsPoolHandler itemsPoolHandler, GameObject item, 
            Transform creationTransform, int itemsCount)
        {
            base.InitializeItemsPool(itemsPoolHandler, item, creationTransform, itemsCount);
        
            _temporaryCoins = new List<AwardCoin>();
        
            foreach (var coinPrefabClone in ItemsPool)
                _temporaryCoins.Add(coinPrefabClone.GetComponent<AwardCoin>());
        }
    
        public void PrepareCutScene()
        {
            InitializeItemsPool(_cutSceneConfig.ItemsPoolHandler, _cutSceneConfig.CoinPrefab.gameObject, 
                _gameInterfaceWindow.ScoresIconTransform, _cutSceneConfig.TemporaryCoinsCount);
        
            _comboValueCutScene.PrepareCutScene();
        }
    }
}