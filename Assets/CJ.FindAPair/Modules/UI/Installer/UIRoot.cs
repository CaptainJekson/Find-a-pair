using System.Collections.Generic;
using CJ.FindAPair.Modules.UI.Windows.Base;
using Doozy.Engine.UI;
using UnityEngine;

namespace CJ.FindAPair.Modules.UI.Installer
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private List<Window> _windows;

        public void OpenWindow<T>() where T : Window
        {
            foreach (var window in _windows)
            {
                if (window.GetType() == typeof(T))
                {
                    var uIView = window.GetComponent<UIView>();
                    UIView.ShowView(uIView.ViewCategory, uIView.ViewName);
                    break;
                }
            }
        }
        
        public void CloseWindow<T>() where T : Window
        {
            foreach (var window in _windows)
            {
                if (window.GetType() == typeof(T))
                {
                    var uIView = window.GetComponent<UIView>();
                    UIView.HideView(uIView.ViewCategory, uIView.ViewName);
                    break;
                }
            }
        }

        public T GetWindow<T>() where T : Window
        {
            foreach (var window in _windows)
            {
                if (window.GetType() == typeof(T))
                {
                    return window as T;
                }
            }

            return null;
        }
    }
}