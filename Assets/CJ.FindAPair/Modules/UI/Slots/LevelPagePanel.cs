using System.Collections.Generic;
using UnityEngine;

namespace CJ.FindAPair.Modules.UI.Slots
{
    class LevelPagePanel : MonoBehaviour
    {
        [SerializeField] private List<LevelSlot> _levelSlots;

        public List<LevelSlot> LevelSlots => _levelSlots;
    }
}
