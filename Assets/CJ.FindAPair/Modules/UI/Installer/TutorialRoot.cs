using System;
using System.Collections.Generic;
using System.Linq;
using CJ.FindAPair.Modules.UI.Tutorial;
using CJ.FindAPair.Modules.UI.Tutorial.Base;
using DG.Tweening;
using UnityEngine;

namespace CJ.FindAPair.Modules.UI.Installer
{
    public class TutorialRoot : MonoBehaviour
    {
        [SerializeField] private List<TutorialScreen> _tutorialScreens;

        private void Awake()
        {
            foreach (var screen in _tutorialScreens)
            {
                screen.gameObject.SetActive(false);
            }
        }

        public void ShowTutorial<T>(float delay = 0.0f) where T : TutorialScreen
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(delay);
            sequence.AppendCallback(() =>
            {
                foreach (var screen in _tutorialScreens.Where(screen => screen.GetType() == typeof(T)))
                {
                    screen.Show();
                    break;
                }
            });
        }

        public T GetScreen<T>() where T : TutorialScreen
        {
            foreach (var window in _tutorialScreens)
            {
                if (window.GetType() == typeof(T))
                {
                    return window as T;
                }
            }

            return null;
        }

        public void SetActionForStep<T>(Action action, int stepIndex) where T : TutorialScreen
        {
            foreach (var screen in _tutorialScreens.Where(screen => screen.GetType() == typeof(T)))
            {
                for (var i = 0; i < screen.Steps.Count; i++)
                {
                    if (stepIndex == i)
                    {
                        screen.Steps[i].SetAction(action);
                        break;
                    }
                }
                
                break;
            }
        }
    }
}
