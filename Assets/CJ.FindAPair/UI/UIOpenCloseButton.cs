﻿using Doozy.Engine.Nody.Models;
using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.UI
{
    [RequireComponent(typeof(Button))]
    public class UIOpenCloseButton : MonoBehaviour
    {
        [SerializeField] private UIView _uIView;
        [SerializeField] private bool _isHide;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ButtonCLickHandler);
        }

        private void ButtonCLickHandler()
        {
            if(_isHide)
            {
                _uIView.Hide();
                UIView.HideView("General", "BlockPanel");
            }
            else
            {
                _uIView.Show();
                UIView.ShowView("General", "BlockPanel");
            }
        }
    }
}