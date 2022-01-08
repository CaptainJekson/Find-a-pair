using System;
using System.Collections.Generic;
using CJ.FindAPair.Modules.UI.Tutorial;
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
    }
}
