using CJ.FindAPair.Modules.CutScene.CutScenes;
using CJ.FindAPair.Modules.UI.Installer;
using Spine.Unity;
using TMPro;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class GiftBoxWindow : Window
    {
        [SerializeField] private SkeletonGraphic _skeletonAnimation;
        [SerializeField] private Transform _topItemsPanelTransform;
        [SerializeField] private Transform _bottomItemsPanelTransform;
        [SerializeField] private Transform _itemsPointerTransform;

        [SerializeField] private TextMeshProUGUI _energyValueText;
        [SerializeField] private TextMeshProUGUI _diamondValueText;
        [SerializeField] private TextMeshProUGUI _coinValueText;
        [SerializeField] private TextMeshProUGUI _detectorValueText;
        [SerializeField] private TextMeshProUGUI _magnetValueText;
        [SerializeField] private TextMeshProUGUI _sapperValueText;

        private BlockWindow _blockWindow;
        private GiftBoxCutScene _giftBoxCutScene;

        public SkeletonGraphic SkeletonAnimation => _skeletonAnimation;
        public Transform TopItemsPanelTransform => _topItemsPanelTransform;
        public Transform BottomItemsPanelTransform => _bottomItemsPanelTransform;
        public Transform ItemsPointerTransform => _itemsPointerTransform;
    
        public TextMeshProUGUI EnergyValueText => _energyValueText;
        public TextMeshProUGUI DiamondValueText => _diamondValueText;
        public TextMeshProUGUI CoinValueText => _coinValueText;
        public TextMeshProUGUI DetectorValueText => _detectorValueText;
        public TextMeshProUGUI MagnetValueText => _magnetValueText;
        public TextMeshProUGUI SapperValueText => _sapperValueText;

        [Inject]
        public void Construct(UIRoot uiRoot, GiftBoxCutScene giftBoxCutScene)
        {
            _blockWindow = uiRoot.GetWindow<BlockWindow>();
            _giftBoxCutScene = giftBoxCutScene;
        }
    
        protected override void OnOpen()
        {
            _blockWindow.SetOpenWindow(this);
            _giftBoxCutScene.Play();
        }

        protected override void OnClose()
        {
            _giftBoxCutScene.Stop();
        }
    }
}