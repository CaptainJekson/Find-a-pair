using System;
using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.Modules.UI.Tutorial
{
    public class TutorialStep : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private Action _action;
        
        private void Awake()
        {
            _button.onClick.AddListener(MakeAction);
        }

        public void SetAction(Action action)
        {
            _action = action;
        }

        private void MakeAction()
        {
            _action?.Invoke();
        }
    }
}
