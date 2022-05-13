﻿using System.Collections.Generic;
using UnityEngine;

namespace CJ.FindAPair.Modules.UI.View
{
    public class LevelLocation : MonoBehaviour
    {
        [SerializeField] private Sprite _levelButtonSprite;
        [SerializeField] private CloudBlockerForLocation _blocker;
        [SerializeField] private List<LevelButton> _levelButtons;

        public bool IsUnlock { get; private set; }

        private void Awake()
        {
            foreach (var button in _levelButtons)
            {
                button.gameObject.SetActive(false);
                button.SetStandardSprite(_levelButtonSprite);
            }
        }

        public void Unlock()
        {
            _blocker.Unlock();
            IsUnlock = true;
        }

        public void UnlockFast()
        {
            _blocker.UnlockFast();
            IsUnlock = true;
        }

        public List<LevelButton> LevelButtons => _levelButtons;
    }
}