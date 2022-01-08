using System.Collections.Generic;
using CJ.FindAPair.Modules.CutScenes.CutScenes;
using CJ.FindAPair.Modules.UI.Installer;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class GiftBoxWindow : Window
    {
        [SerializeField] private SkeletonGraphic _skeletonAnimation;
        [SerializeField] private Transform _topItemsPanelTransform;
        [SerializeField] private Transform _bottomItemsPanelTransform;
        [SerializeField] private Transform _itemsPointerTransform;
        [SerializeField] private List<GameResourceItem> _gameResourceItems;
        [SerializeField] private Button _resumeButton;

        private UIRoot _uiRoot;
        private BlockWindow _blockWindow;
        private GiftBoxCutScene _giftBoxCutScene;

        public SkeletonGraphic SkeletonAnimation => _skeletonAnimation;
        public Transform TopItemsPanelTransform => _topItemsPanelTransform;
        public Transform BottomItemsPanelTransform => _bottomItemsPanelTransform;
        public Transform ItemsPointerTransform => _itemsPointerTransform;
        public List<GameResourceItem> GameResourceItems => _gameResourceItems;
        public Button ResumeButton => _resumeButton;

        public event UnityAction WindowClosed;

        [Inject]
        public void Construct(UIRoot uiRoot, GiftBoxCutScene giftBoxCutScene)
        {
            _uiRoot = uiRoot;
            _blockWindow = uiRoot.GetWindow<BlockWindow>();
            _giftBoxCutScene = giftBoxCutScene;
        }
    
        protected override void OnOpen()
        {
            _blockWindow.SetOpenWindow(this);
            _blockWindow.SetCloseButtonCondition(false);
            _giftBoxCutScene.Play();
        }

        protected override void OnClose()
        {
            _blockWindow.SetCloseButtonCondition(true);
            _blockWindow.Close();
            _giftBoxCutScene.Stop();
            _resumeButton.gameObject.SetActive(false);
            _uiRoot.OpenWindow<MenuButtonsWindow>();
            WindowClosed?.Invoke();
        }
    }
}