using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames.Booster;
using CJ.FindAPair.Modules.Service.Save;
using CJ.FindAPair.Modules.UI.Slots;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class BoosterInterfaceWindow : Window
    {
        [SerializeField] private List<BoosterButton> _boosterButtons;

        private BoosterHandler _boosterHandler;
        private GameSaver _gameSaver;
        
        [Inject]
        public void Construct(BoosterHandler boosterHandler, GameSaver gameSaver)
        {
            _boosterHandler = boosterHandler;
            _gameSaver = gameSaver;
        }

        public void RefreshButtons()
        {
            foreach (var boosterButton in _boosterButtons)
            {
                boosterButton.SetCounter();
            }
        }
        
        protected override void Init()
        {
            foreach (var boosterButton in _boosterButtons)
            {
                boosterButton.Init(_boosterHandler, _gameSaver);
            }
        }
    }
}