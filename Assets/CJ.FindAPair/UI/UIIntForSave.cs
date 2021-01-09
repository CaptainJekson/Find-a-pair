using System;
using CJ.FindAPair.Game;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

namespace CJ.FindAPair.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UIIntForSave : MonoBehaviour
    {
        [SerializeField] private PlayerResourcesType playerResourcesType;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _text.SetText(GameSaver.LoadResources(playerResourcesType).ToString());
        }

        private void OnEnable()
        {
            GameSaver.OnSaved += SetText;
        }

        private void OnDisable()
        {
            GameSaver.OnSaved += SetText;
        }

        private void SetText()
        {
            _text.text = GameSaver.LoadResources(playerResourcesType).ToString();
        }
    }
}