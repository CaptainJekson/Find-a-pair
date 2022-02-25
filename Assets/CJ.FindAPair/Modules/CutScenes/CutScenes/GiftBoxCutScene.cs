using System;
using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.CutScenes.Configs;
using CJ.FindAPair.Modules.CutScenes.CutScenes.Base;
using CJ.FindAPair.Modules.CutScenes.Installer;
using CJ.FindAPair.Modules.Service.Audio;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;
using DG.Tweening;
using UnityEngine;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes
{
    public class GiftBoxCutScene : AbstractCutScene
    {
        private UIRoot _uiRoot;
        private ISaver _gameSaver;
        private GiftBoxCutSceneConfig _cutSceneConfig;
        private PlayerResourcesWindow _playerResourcesWindow;
        private BlockWindow _blockWindow;
        private GiftBoxWindow _giftBoxWindow;
        private ItemsTransferer _itemsTransferer;
        private LevelCreator _levelCreator;
        private AudioController _audioController;
        
        private Sequence _giftBoxOpenSequence;
        private List<GiftItem> _giftItems;

        public GiftBoxCutScene(UIRoot uiRoot, ISaver gameSaver, LevelCreator levelCreator,
            CutScenesConfigs cutScenesConfigs, ItemsTransferer itemsTransferer, AudioController audioController)
        {
            _uiRoot = uiRoot;
            _gameSaver = gameSaver;
            _cutSceneConfig = cutScenesConfigs.GetConfig<GiftBoxCutSceneConfig>();
            _playerResourcesWindow = uiRoot.GetWindow<PlayerResourcesWindow>();
            _blockWindow = uiRoot.GetWindow<BlockWindow>();
            _giftBoxWindow = uiRoot.GetWindow<GiftBoxWindow>();
            _itemsTransferer = itemsTransferer;
            _levelCreator = levelCreator;
            _audioController = audioController;
        }
    
        public override void Play()
        {
            _giftBoxOpenSequence = DOTween.Sequence();

            var rewardItems = _levelCreator.LevelConfig.RewardItemsCollection.Items;

            InitializeItemsPool(_cutSceneConfig.ItemsPoolHandler, _cutSceneConfig.GiftItemPrefab.gameObject, 
                _giftBoxWindow.ItemsPointerTransform, rewardItems.Count);
            
            SetGiftsShowPointers();
            
            for (int i = 0; i < _giftItems.Count; i++)
                _giftItems[i].SetItem(rewardItems[i].Icon, rewardItems[i].Count);
            
            _giftBoxOpenSequence
                .AppendCallback(() =>
                {
                    _playerResourcesWindow.Close();
                    _uiRoot.CloseWindow<MenuButtonsWindow>();
                })
                .AppendInterval(_cutSceneConfig.DelayCutSceneStart)
                .AppendCallback(() =>
                {
                    _blockWindow.Open();
                    TransferItemsPanels();
                    SetBoxAnimations();
                    _audioController.PlaySound(_audioController.AudioClipsCollection.GiftBoxOpenSound);
                })
                .AppendInterval(_cutSceneConfig.OpenAnimationDuration)
                .AppendCallback(ShowGifts)
                .AppendInterval(_cutSceneConfig.ObtainTransferDuration * _giftItems.Count)
                .AppendCallback(() => _giftBoxWindow.ResumeButton.gameObject.SetActive(true))
                .AppendInterval(_cutSceneConfig.ShowingDuration)
                .AppendCallback(() => ObtainGiftItems(rewardItems))
                .AppendInterval(_cutSceneConfig.ObtainTransferDuration)
                .AppendCallback(() =>
                {
                    foreach (var item in _giftItems)
                    {
                        _audioController.PlaySound(_audioController.AudioClipsCollection.GiftItemObtainSound, true);
                        item.gameObject.SetActive(false);
                    }
                });
        }

        public override void Stop()
        {
            _giftBoxOpenSequence.Kill();
            _playerResourcesWindow.Open();
            _giftBoxWindow.SkeletonAnimation.AnimationState.ClearTracks();
            _giftBoxWindow.TopItemsPanelTransform.gameObject.SetActive(false);
            _giftBoxWindow.BottomItemsPanelTransform.gameObject.SetActive(false);
            _giftBoxWindow.SkeletonAnimation.gameObject.SetActive(false);
            _giftItems.Clear();
            _cutSceneConfig.ItemsPoolHandler.DestroyItemsPool(ItemsPool);
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

        private void SetBoxAnimations()
        {
            _giftBoxWindow.SkeletonAnimation.gameObject.SetActive(true);
            _giftBoxWindow.SkeletonAnimation.AnimationState
                .SetAnimation(0, _cutSceneConfig.NameAnimationOpen, false);
            _giftBoxWindow.SkeletonAnimation.AnimationState
                .AddAnimation(1, _cutSceneConfig.NameAnimationIdle, true, _cutSceneConfig.OpenAnimationDuration);
        }

        private void SetGiftsShowPointers()
        {
            var itemsCounter = 0;

            bool isEven = _giftItems.Count % 2 == 0;

            if (isEven)
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

        private void ObtainGiftItems(List<ItemConfig> rewardItems)
        {
            var itemsData = _gameSaver.LoadData().ItemsData;
            
            for (int i = 0; i < _giftItems.Count; i++)
            {
                switch (rewardItems[i].Type)
                {
                    case ItemTypes.Energy:
                        ObtainItem(_giftItems[i], GetResourceItem(ItemTypes.Energy), itemsData.Energy);
                        break;
                    case ItemTypes.Diamond:
                        ObtainItem(_giftItems[i], GetResourceItem(ItemTypes.Diamond), itemsData.Diamond);
                        break;
                    case ItemTypes.Coin:
                        ObtainItem(_giftItems[i], GetResourceItem(ItemTypes.Coin), itemsData.Coins);
                        break;
                    case ItemTypes.DetectorBooster:
                        ObtainItem(_giftItems[i], GetResourceItem(ItemTypes.DetectorBooster), itemsData.DetectorBooster);
                        break;
                    case ItemTypes.MagnetBooster:
                        ObtainItem(_giftItems[i], GetResourceItem(ItemTypes.MagnetBooster), itemsData.MagnetBooster);
                        break;
                    case ItemTypes.SapperBooster:
                        ObtainItem(_giftItems[i], GetResourceItem(ItemTypes.SapperBooster), itemsData.SapperBooster);
                        break;
                }
            }
        }
        
        private void ObtainItem(GiftItem giftItem, GameResourceItem gameResourceItem, int endValue)
        {
            Sequence obtainItemSequence = DOTween.Sequence();

            obtainItemSequence
                .AppendCallback(() =>
                {
                    _itemsTransferer.TransferItem(giftItem.gameObject.transform, giftItem.transform.position,
                        gameResourceItem.transform.position, _cutSceneConfig.ObtainTransferDuration,
                        _cutSceneConfig.ObtainTransferEase);
                    gameResourceItem.SmoothChangeValue(endValue, _cutSceneConfig.ValueChangeDuration,
                        _cutSceneConfig.ValueChangeEase);
                });
        }

        private GameResourceItem GetResourceItem(ItemTypes itemType)
        {
            foreach (var item in _giftBoxWindow.GameResourceItems)
            {
                if (item.Type == itemType)
                    return item;
            }

            return null;
        }
    }
}