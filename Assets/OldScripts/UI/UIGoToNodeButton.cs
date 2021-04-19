using Doozy.Engine.Nody;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.UI
{
    [RequireComponent(typeof(Button))]
    public class UIGoToNodeButton : MonoBehaviour
    {
        [SerializeField] private GraphController _myController;
        [SerializeField] private string _nodeName;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ButtonCLickHandler);
        }

        private void ButtonCLickHandler()
        {
            _myController.GoToNodeByName(_nodeName);
        }
    }
}

