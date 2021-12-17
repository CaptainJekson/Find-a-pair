using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.Modules.CoreGames.Configs
{
    [CreateAssetMenu(fileName = "Item", menuName = "Find a pair/Item")]
    public class ItemConfig : ScriptableObject
    {
        [SerializeField] private ItemTypes _type;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _count;

        public ItemTypes Type => _type;
        public Sprite Icon => _icon;
        public int Count => _count;
    }
}