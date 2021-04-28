using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames.Booster;
using CJ.FindAPair.Modules.UI.Slots;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class BoosterInterfaceWindow : Window
    {
        [SerializeField] private List<BoosterButton> _boosterButtons;

        private BoosterHandler _boosterHandler;
        
        [Inject]
        public void Construct(BoosterHandler boosterHandler)
        {
            _boosterHandler = boosterHandler;
        }
        
        protected override void Init()
        {
            foreach (var boosterButton in _boosterButtons)
            {
                boosterButton.Init(_boosterHandler);
            }
        }
    }
}