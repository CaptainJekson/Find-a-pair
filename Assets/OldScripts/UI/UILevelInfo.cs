using System;
using CJ.FindAPair.CoreGames;
using CJ.FindAPair.Modules.CoreGames;
using TMPro;
using UnityEngine;

namespace CJ.FindAPair.UI
{
    public class UILevelInfo : MonoBehaviour
    {
        [SerializeField] private LevelCreator _levelCreator;
        [SerializeField] private TextMeshProUGUI _levelText;

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
            _levelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
        }
    }
}