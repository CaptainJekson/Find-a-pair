using CJ.FindAPair.Modules.CutScenes.Configs.Base;
using DG.Tweening;
using UnityEngine;

namespace CJ.FindAPair.Modules.CutScenes.Configs
{
    [CreateAssetMenu(fileName = "GiftBoxCutSceneConfig", 
        menuName = "Find a pair/CutScenesConfigs/GiftBoxCutSceneConfig")]
    public class GiftBoxCutSceneConfig : CutSceneConfig
    {
        [SerializeField] private ItemsPoolHandler _itemsPoolHandler;
        [SerializeField] private GiftItem _giftItemPrefab;
        
        [SerializeField] private float _delayCutSceneStart;
        [SerializeField] private float _xAxisPanelIndent;
        [SerializeField] private float _panelsTransferDuration;
        
        [SerializeField] private string _nameAnimationOpen;
        [SerializeField] private string _nameAnimationIdle;
        [SerializeField] private float _openAnimationDuration;
        
        [SerializeField] private int _paddingModifier;
        [SerializeField] private float _intervalBetweenGiftsTransfers;
        [SerializeField] private float _showingDuration;
        [SerializeField] private float _showingPointTransferDuration;
        [SerializeField] private Ease _showingPointTransferEase;
        
        [SerializeField] private float _obtainTransferDuration;
        [SerializeField] private Ease _obtainTransferEase;
        [SerializeField] private float _valueChangeDuration;
        [SerializeField] private Ease _valueChangeEase;
        
        public ItemsPoolHandler ItemsPoolHandler => _itemsPoolHandler;
        public GiftItem GiftItemPrefab => _giftItemPrefab;
        
        public float DelayCutSceneStart => _delayCutSceneStart;
        public float XAxisPanelIndent => _xAxisPanelIndent;
        public float PanelsTransferDuration => _panelsTransferDuration;
        
        public string NameAnimationOpen => _nameAnimationOpen;
        public string NameAnimationIdle => _nameAnimationIdle;
        public float OpenAnimationDuration => _openAnimationDuration;
        
        public int PaddingModifier => _paddingModifier;
        public float IntervalBetweenGiftsTransfers => _intervalBetweenGiftsTransfers;
        public float ShowingDuration => _showingDuration;
        public float ShowingPointTransferDuration => _showingPointTransferDuration;
        public Ease ShowingPointTransferEase => _showingPointTransferEase;
        
        public float ObtainTransferDuration => _obtainTransferDuration;
        public Ease ObtainTransferEase => _obtainTransferEase;
        public float ValueChangeDuration => _valueChangeDuration;
        public Ease ValueChangeEase => _valueChangeEase;
    }
}