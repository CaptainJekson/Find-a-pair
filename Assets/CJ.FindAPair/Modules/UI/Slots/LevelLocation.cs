using System;
using System.Collections.Generic;
using UnityEngine;

namespace CJ.FindAPair.Modules.UI.Slots
{
    public class LevelLocation : MonoBehaviour
    {
        [SerializeField] private List<LevelButton> _levelButtons;

        private void Awake()
        {
            foreach (var button in _levelButtons)
            {
                button.gameObject.SetActive(false);
            }
        }

        public List<LevelButton> LevelButtons => _levelButtons;
    }
}
