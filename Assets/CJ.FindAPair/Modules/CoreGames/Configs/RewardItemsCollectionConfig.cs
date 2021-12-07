using System.Collections.Generic;
using UnityEngine;

namespace CJ.FindAPair.Modules.CoreGames.Configs
{
    [CreateAssetMenu(fileName = "ItemsCollection", menuName = "Find a pair/ItemsCollection")]
    public class RewardItemsCollectionConfig : ScriptableObject
    {
        [SerializeField] private List<ItemConfig> _items;

        public List<ItemConfig> Items => _items;
    }
}