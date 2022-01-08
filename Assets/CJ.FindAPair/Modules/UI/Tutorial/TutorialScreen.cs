using System;
using System.Collections.Generic;
using UnityEngine;

namespace CJ.FindAPair.Modules.UI.Tutorial
{
    public class TutorialScreen : MonoBehaviour
    {
        [SerializeField] private List<TutorialStep> _steps;

        private void Awake()
        {
            foreach (var step in _steps)
            {
                step.gameObject.SetActive(false);
            }
        }

        //Показать первый шаг туториала
        private void Show()
        {
            
        }

        //Показать след шаг туториала или закрыть туториал если это был последний шаг
        private void ShowNextStep()
        {
            
        }
    }
}
