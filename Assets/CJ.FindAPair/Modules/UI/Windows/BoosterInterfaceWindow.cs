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
        [SerializeField] private float _boosterCooldownTime;

        private BoosterHandler _boosterHandler;
        private ISaver _gameSaver;

        [Inject]
        public void Construct(BoosterHandler boosterHandler, ISaver gameSaver)
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

        public void CooldownBoosters()
        {
            foreach (var boosterButton in _boosterButtons)
            {
                if (boosterButton.GetButtonBoosterType() == BoosterType.Detector ||
                    boosterButton.GetButtonBoosterType() == BoosterType.Magnet)
                    boosterButton.TryActivateCooldown(_boosterCooldownTime);
            }
        }
    }
}