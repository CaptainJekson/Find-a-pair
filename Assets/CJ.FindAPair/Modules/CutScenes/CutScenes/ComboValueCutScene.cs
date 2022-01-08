using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CutScenes.Configs;
using CJ.FindAPair.Modules.CutScenes.CutScenes.Base;
using CJ.FindAPair.Modules.CutScenes.Installer;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes
{
    public class ComboValueCutScene : CutScene
    {
        private GameWatcher _gameWatcher;
        private CardComparator _cardComparator;
        private GameInterfaceWindow _gameInterfaceWindow;
        private ComboValueCutSceneConfig _cutSceneConfig;
        private ItemsTransferer _itemsTransferer;
        private Camera _camera;

        private Sequence _comboTextShowSequence;
        private List<TextMeshProUGUI> _temporaryTextFrames;
    
        public ComboValueCutScene(GameWatcher gameWatcher, CardComparator cardComparator, UIRoot uiRoot, 
            ItemsTransferer itemsTransferer, CutScenesConfigs cutScenesConfigs)
        {
            _gameWatcher = gameWatcher;
            _cardComparator = cardComparator;
            _itemsTransferer = itemsTransferer;
            _gameInterfaceWindow = uiRoot.GetWindow<GameInterfaceWindow>();
            _cutSceneConfig = cutScenesConfigs.GetConfig<ComboValueCutSceneConfig>();
            _camera = Camera.main;
        }
    
        public override void Play()
        {
            _comboTextShowSequence = DOTween.Sequence();
        
            var startPosition = _camera.WorldToScreenPoint(_cardComparator
                .ComparisonCards[_cardComparator.ComparisonCards.Count - 1].transform.position);
        
            var endPosition = new Vector3(startPosition.x, startPosition.y + _cutSceneConfig.ValueRecoilDistance, 
                startPosition.z);

            foreach (var availableTextFrame in _temporaryTextFrames)
            {
                if (availableTextFrame.gameObject.activeSelf == false)
                {
                    availableTextFrame.SetText($"+{_gameWatcher.ScoreCombo}");
                
                    _comboTextShowSequence
                        .AppendCallback(() => _itemsTransferer.TransferItem(availableTextFrame.transform, 
                            startPosition, endPosition, _cutSceneConfig.ValueShowDuration, 
                            _cutSceneConfig.ValueShowEase))
                        .AppendInterval(_cutSceneConfig.ValueShowDuration)
                        .AppendCallback(() => availableTextFrame.gameObject.SetActive(false));

                    break;
                }
            }
        }

        public override void Stop()
        {
            _comboTextShowSequence.Kill();
            _cutSceneConfig.ItemsPoolHandler.DestroyItemsPool(ItemsPool);
            _temporaryTextFrames.Clear();
        }

        protected override void InitializeItemsPool(ItemsPoolHandler itemsPoolHandler, GameObject item, 
            Transform creationTransform, int itemsCount)
        {
            base.InitializeItemsPool(itemsPoolHandler, item, creationTransform, itemsCount);
        
            _temporaryTextFrames = new List<TextMeshProUGUI>();
        
            foreach (var coinPrefabClone in ItemsPool)
                _temporaryTextFrames.Add(coinPrefabClone.GetComponent<TextMeshProUGUI>());
        }
    
        public void PrepareCutScene()
        {
            InitializeItemsPool(_cutSceneConfig.ItemsPoolHandler, _cutSceneConfig.ValueFramePrefab.gameObject, 
                _gameInterfaceWindow.transform, _cutSceneConfig.TemporaryFramesCount);
        }
    }
}