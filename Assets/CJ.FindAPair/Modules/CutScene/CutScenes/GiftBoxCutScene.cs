using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.CutScene.Configs;
using CJ.FindAPair.Modules.CutScene.Installer;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;
using DG.Tweening;
using UnityEngine;

namespace CJ.FindAPair.Modules.CutScene.CutScenes
{
    public class GiftBoxCutScene : Base.CutScene
    {
        private PlayerResourcesWindow _playerResourcesWindow;
        private BlockWindow _blockWindow;
        private GiftBoxWindow _giftBoxWindow;
        private GiftBoxCutSceneConfig _cutSceneConfig;
        private ItemsTransferer _itemsTransferer;
        private LevelConfig _levelConfig;
        private ISaver _gameSaver;

        private Sequence _giftBoxOpenSequence;
        private List<GiftItem> _giftItems;

        public GiftBoxCutScene(UIRoot uiRoot, CutScenesConfigs cutScenesConfigs, ItemsTransferer itemsTransferer,
            LevelConfigCollection levelConfigCollection, ISaver gameSaver)
        {
            _playerResourcesWindow = uiRoot.GetWindow<PlayerResourcesWindow>();
            _blockWindow = uiRoot.GetWindow<BlockWindow>();
            _giftBoxWindow = uiRoot.GetWindow<GiftBoxWindow>();
            _cutSceneConfig = cutScenesConfigs.GetConfig<GiftBoxCutSceneConfig>();
            _itemsTransferer = itemsTransferer;
            _levelConfig = levelConfigCollection.Levels[gameSaver.LoadData().CurrentLevel - 1];
            _gameSaver = gameSaver;
        }
    
        public override void Play()
        {
            _giftBoxOpenSequence = DOTween.Sequence();
            
            var rewardItems = _levelConfig.RewardItemsCollection.Items;
            
            LoadGameResources();
            
            InitializeItemsPool(_cutSceneConfig.ItemsPoolHandler, _cutSceneConfig.GiftItemPrefab.gameObject, 
                _giftBoxWindow.ItemsPointerTransform, rewardItems.Count);
            
            SetGiftsShowPoints();
            
            for (int i = 0; i < _giftItems.Count; i++)
                _giftItems[i].SetItem(rewardItems[i].Icon, rewardItems[i].Count);
            
            _giftBoxOpenSequence
                .AppendCallback(_playerResourcesWindow.Close)
                .AppendInterval(_cutSceneConfig.DelayCutSceneStart)
                .AppendCallback(() =>
                {
                    _blockWindow.Open();
                    TransferItemsPanels();
                    
                    _giftBoxWindow.SkeletonAnimation.gameObject.SetActive(true);
                    _giftBoxWindow.SkeletonAnimation.AnimationState
                        .SetAnimation(0, _cutSceneConfig.NameAnimationOpen, false);
                    _giftBoxWindow.SkeletonAnimation.AnimationState
                        .AddAnimation(1, _cutSceneConfig.NameAnimationIdle, true, _cutSceneConfig.OpenAnimationDuration);
                })
                .AppendInterval(_cutSceneConfig.OpenAnimationDuration)
                .AppendCallback(ShowGifts)
                .AppendInterval(_cutSceneConfig.ObtainTransferDuration * _giftItems.Count + _cutSceneConfig.ShowingDuration)
                .AppendCallback(() =>
                {
                    for (int i = 0; i < _giftItems.Count; i++)
                    {
                        switch (rewardItems[i].Type)
                        {
                            case ItemTypes.Energy:
                                TransferItemToPanel(_giftItems[i], 
                                    _giftBoxWindow.EnergyValueText.gameObject.transform.position);
                                break;
                            case ItemTypes.Diamond:
                                TransferItemToPanel(_giftItems[i], 
                                    _giftBoxWindow.DiamondValueText.gameObject.transform.position);
                                break;
                            case ItemTypes.Coin:
                                TransferItemToPanel(_giftItems[i], 
                                    _giftBoxWindow.CoinValueText.gameObject.transform.position);
                                break;
                            case ItemTypes.DetectorBooster:
                                TransferItemToPanel(_giftItems[i], 
                                    _giftBoxWindow.DetectorValueText.gameObject.transform.position);
                                break;
                            case ItemTypes.MagnetBooster:
                                TransferItemToPanel(_giftItems[i], 
                                    _giftBoxWindow.MagnetValueText.gameObject.transform.position);
                                break;
                            case ItemTypes.SapperBooster:
                                TransferItemToPanel(_giftItems[i], 
                                    _giftBoxWindow.SapperValueText.gameObject.transform.position);
                                break;
                        }
                    }
                });
        }

        public override void Stop()
        {
            _giftBoxOpenSequence.Kill();
            _playerResourcesWindow.Open();
            _giftBoxWindow.TopItemsPanelTransform.gameObject.SetActive(false);
            _giftBoxWindow.BottomItemsPanelTransform.gameObject.SetActive(false);
            _giftBoxWindow.SkeletonAnimation.gameObject.SetActive(false);
            _cutSceneConfig.ItemsPoolHandler.DestroyItemsPool(ItemsPool);
            _giftItems.Clear();
        }

        protected override void InitializeItemsPool(ItemsPoolHandler itemsPoolHandler, GameObject item, 
            Transform creationTransform, int itemsCount)
        {
            base.InitializeItemsPool(itemsPoolHandler, item, creationTransform, itemsCount);
        
            _giftItems = new List<GiftItem>();
        
            foreach (var giftItemClone in ItemsPool)
                _giftItems.Add(giftItemClone.GetComponent<GiftItem>());
        }
        
        private void TransferItemsPanels()
        {
            var topPanelStartPosition = new Vector3(_cutSceneConfig.XAxisPanelIndent, 
                _giftBoxWindow.TopItemsPanelTransform.position.y, _giftBoxWindow.TopItemsPanelTransform.position.z);
            var bottomPanelStartPosition = new Vector3(-_cutSceneConfig.XAxisPanelIndent, 
                _giftBoxWindow.BottomItemsPanelTransform.position.y, _giftBoxWindow.BottomItemsPanelTransform.position.z);
            
            _itemsTransferer.TransferItem(_giftBoxWindow.TopItemsPanelTransform, topPanelStartPosition,
                _giftBoxWindow.TopItemsPanelTransform.transform.position, _cutSceneConfig.PanelsTransferDuration);
            _itemsTransferer.TransferItem(_giftBoxWindow.BottomItemsPanelTransform, bottomPanelStartPosition,
                _giftBoxWindow.BottomItemsPanelTransform.transform.position, _cutSceneConfig.PanelsTransferDuration);
        }

        private void SetGiftsShowPoints()
        {
            var itemsCounter = 0;
            
            if (_giftItems.Count % 2 == 0)
            {
                for (int i = 0; i < _giftItems.Count / 2; i++)
                {
                    _giftItems[i].transform.localPosition =
                        new Vector3(i * 2 * _cutSceneConfig.PaddingModifier + _cutSceneConfig.PaddingModifier, 0, 0);
                }

                for (int i = _giftItems.Count / 2; i < _giftItems.Count; i++)
                    _giftItems[i].transform.localPosition = -_giftItems[itemsCounter++].transform.localPosition;
            }
            else
            {
                for (int i = 1; i < (_giftItems.Count + 1) / 2; i++)
                    _giftItems[i].transform.localPosition = new Vector3(i * 2 * _cutSceneConfig.PaddingModifier, 0, 0);
                
                for (int i = (_giftItems.Count + 1) / 2; i < _giftItems.Count; i++)
                    _giftItems[i].transform.localPosition = -_giftItems[++itemsCounter].transform.localPosition;
            }
        }

        private void ShowGifts()
        {
            Sequence showingSequence = DOTween.Sequence();
            
            foreach (var item in _giftItems)
            {
                showingSequence
                    .AppendCallback(() =>
                        _itemsTransferer.TransferItem(item.gameObject.transform, 
                            _giftBoxWindow.SkeletonAnimation.transform.position, 
                            item.transform.position, _cutSceneConfig.ShowingPointTransferDuration, 
                            _cutSceneConfig.ShowingPointTransferEase))
                    .AppendInterval(_cutSceneConfig.IntervalBetweenGiftsTransfers);;
            }
        }
        
        private void TransferItemToPanel(GiftItem item, Vector3 endPosition)
        {
            _itemsTransferer.TransferItem(item.gameObject.transform, item.transform.position, endPosition, 
                _cutSceneConfig.ObtainTransferDuration, _cutSceneConfig.ObtainTransferEase);
        }

        private void LoadGameResources()
        {
            var saveData = _gameSaver.LoadData().ItemsData;
            var itemsCollection = _levelConfig.RewardItemsCollection.Items;
            
            foreach (var item in itemsCollection)
            {
                switch (item.Type)
                {
                    case ItemTypes.Coin:
                        _giftBoxWindow.CoinValueText.SetText((saveData.Coins - item.Count).ToString());
                        break;
                    case ItemTypes.Diamond:
                        _giftBoxWindow.DiamondValueText.SetText((saveData.Diamond - item.Count).ToString());
                        break;
                    case ItemTypes.Energy:
                        _giftBoxWindow.EnergyValueText.SetText((saveData.Energy - item.Count).ToString());
                        break;
                    case ItemTypes.DetectorBooster:
                        _giftBoxWindow.DetectorValueText.SetText((saveData.DetectorBooster - item.Count).ToString());
                        break;
                    case ItemTypes.MagnetBooster:
                        _giftBoxWindow.MagnetValueText.SetText((saveData.MagnetBooster - item.Count).ToString());
                        break;
                    case ItemTypes.SapperBooster:
                        _giftBoxWindow.SapperValueText.SetText((saveData.SapperBooster - item.Count).ToString());
                        break;
                }
            }
        }
    }
}