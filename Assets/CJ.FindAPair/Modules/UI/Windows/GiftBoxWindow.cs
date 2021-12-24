using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;
using Zenject;

public class GiftBoxWindow : Window
{
    [SerializeField] private SkeletonGraphic _skeletonAnimation;
    [SerializeField, SpineAnimation] private string _nameAnimationOpen;
    
    [SerializeField] private Transform _topItemsPanelTransform;
    [SerializeField] private Transform _bottomItemsPanelTransform;
    [SerializeField] private float _xAxisPanelStartPosition;
    
    private BlockWindow _blockWindow;
    private PlayerResourcesWindow _playerResourcesWindow;
    private ItemsTransferer _itemsTransferer;
    
    [Inject]
    public void Construct(UIRoot uiRoot, ItemsTransferer itemsTransferer)
    {
        _blockWindow = uiRoot.GetWindow<BlockWindow>();
        _playerResourcesWindow = uiRoot.GetWindow<PlayerResourcesWindow>();
        _itemsTransferer = itemsTransferer;
    }
    
    protected override void OnOpen()
    {
        _blockWindow.SetOpenWindow(this);
        
        StartGiftBoxCutScene();
    }

    private void StartGiftBoxCutScene()
    {
        Sequence sequence = DOTween.Sequence();

        var topPanelStartPosition = new Vector3(_xAxisPanelStartPosition, 
            _topItemsPanelTransform.position.y, _topItemsPanelTransform.position.z);
        var bottomPanelStartPosition = new Vector3(-_xAxisPanelStartPosition, 
            _bottomItemsPanelTransform.position.y, _bottomItemsPanelTransform.position.z);
        
        sequence
            .AppendCallback(_playerResourcesWindow.Close)
            .AppendInterval(0.25f)
            .AppendCallback(() =>
            {
                _blockWindow.Open();
                _itemsTransferer.TransferItem(_topItemsPanelTransform, topPanelStartPosition,
                    _topItemsPanelTransform.transform.position, 1.25f);
                _itemsTransferer.TransferItem(_bottomItemsPanelTransform, bottomPanelStartPosition,
                    _bottomItemsPanelTransform.transform.position, 1.25f);
                _skeletonAnimation.gameObject.SetActive(true);
                _skeletonAnimation.AnimationState.SetAnimation(0, _nameAnimationOpen, false).MixDuration = 0;
                _skeletonAnimation.Update(0f);
            });
    }
}