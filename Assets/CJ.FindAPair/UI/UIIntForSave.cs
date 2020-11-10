using System;
using CJ.FindAPair.Game;
using UnityEngine;
using TMPro;

namespace CJ.FindAPair.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UIIntForSave : MonoBehaviour
    {
        [SerializeField] private SaveTypeInt _saveTypeInt;
        [SerializeField] private GameSaver _gameSaver;
        
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _gameSaver.OnSaved += SetText;
        }

        private void OnDisable()
        {
            _gameSaver.OnSaved += SetText;
        }

        private void SetText()
        {
            _text.text = _gameSaver.LoadInt(_saveTypeInt).ToString();
        }
    }
}