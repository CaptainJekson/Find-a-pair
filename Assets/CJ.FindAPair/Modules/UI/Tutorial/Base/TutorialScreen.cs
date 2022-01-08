using System.Collections.Generic;
using UnityEngine;

namespace CJ.FindAPair.Modules.UI.Tutorial.Base
{
    public class TutorialScreen : MonoBehaviour
    {
        [SerializeField] private List<TutorialStep> _steps;

        private void Awake()
        {
            foreach (var step in _steps)
            {
                step.gameObject.SetActive(false);
                step.Init(this);
            }
        }
        
        public void Show()
        {
            _steps[0].Show();
            gameObject.SetActive(true);
        }
        
        public void ShowNextStep()
        {
            for (var i = 0; i < _steps.Count; i++)
            {
                if (_steps[i].gameObject.activeSelf)
                {
                    _steps[i].Hide();

                    if (i < _steps.Count - 1)
                    {
                        _steps[i + 1].Show();
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    
                    break;
                }
            }
        }
    }
}
