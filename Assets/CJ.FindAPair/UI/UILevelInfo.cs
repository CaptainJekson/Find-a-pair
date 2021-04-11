﻿using CJ.FindAPair.CardTable;
using TMPro;
using UnityEngine;

namespace CJ.FindAPair.UI
{
    public class UILevelInfo : MonoBehaviour
    {
        [SerializeField] private LevelCreator _levelCreator;
        [SerializeField] private TextMeshProUGUI _currentLevelText;

        private void Awake()
        {
            _levelCreator.OnLevelCreated += SetData;
        }

        private void OnDestroy()
        {
            _levelCreator.OnLevelCreated -= SetData;
        }

        private void SetData()
        {
            _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
        }
    }
}