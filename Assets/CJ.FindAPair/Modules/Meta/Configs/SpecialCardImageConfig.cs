using UnityEngine;

namespace CJ.FindAPair.Modules.Meta.Configs
{
    [CreateAssetMenu(fileName = "SpecialCardImageConfig", menuName = "Find a pair/SpecialCardImageConfig")]
    public class SpecialCardImageConfig : ScriptableObject
    {
        [SerializeField] private Sprite _fortuneSprite;
        [SerializeField] private Sprite _entanglementSprite;
        [SerializeField] private Sprite _resetSprite;
        [SerializeField] private Sprite _bombSprite;
        
        public Sprite FortuneSprite => _fortuneSprite;
        public Sprite EntanglementSprite => _entanglementSprite;
        public Sprite ResetSprite => _resetSprite;
        public Sprite BombSprite => _bombSprite;
    }
}
