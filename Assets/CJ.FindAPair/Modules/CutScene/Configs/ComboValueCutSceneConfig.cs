using CJ.FindAPair.Modules.CutScene.Configs.Base;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CJ.FindAPair.Modules.CutScene.Configs
{
    [CreateAssetMenu(fileName = "ComboValueCutSceneConfig", 
        menuName = "Find a pair/CutScenesConfigs/ComboValueCutSceneConfig")]
    public class ComboValueCutSceneConfig : CutSceneConfig
    {
        [SerializeField] private ItemsPoolHandler _itemsPoolHandler;
        [SerializeField] private TextMeshProUGUI _valueFramePrefab;
        [SerializeField] private int _temporaryFramesCount;
        [SerializeField] private float _valueRecoilDistance;
        [SerializeField] private float _valueShowDuration;
        [SerializeField] private Ease _valueShowEase;
    
        public ItemsPoolHandler ItemsPoolHandler => _itemsPoolHandler;
        public TextMeshProUGUI ValueFramePrefab => _valueFramePrefab;
        public int TemporaryFramesCount => _temporaryFramesCount;
        public float ValueRecoilDistance => _valueRecoilDistance;
        public float ValueShowDuration => _valueShowDuration;
        public Ease ValueShowEase => _valueShowEase;
    }
}