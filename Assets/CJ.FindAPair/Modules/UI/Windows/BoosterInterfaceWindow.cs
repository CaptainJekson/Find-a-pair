using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
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
        private LevelCreator _levelCreator;
        private ISaver _gameSaver;

        [Inject]
        public void Construct(BoosterHandler boosterHandler, ISaver gameSaver, LevelCreator levelCreator)
        {
            _boosterHandler = boosterHandler;
            _gameSaver = gameSaver;
            _levelCreator = levelCreator;
        }

        private void Start()
        {
            _levelCreator.OnLevelCreated += TryDisableSapperButton;
        }

        public void RefreshButtons()
        {
            foreach (var boosterButton in _boosterButtons)
            {
                boosterButton.SetCounter();
            }

            TryDisableSapperButton();
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

        public void TryDisableSapperButton()
        {
            foreach (var boosterButton in _boosterButtons)
            {
                if (boosterButton.GetButtonBoosterType() == BoosterType.Sapper &&
                    !_levelCreator.IsSpecialCardsOnLevel())
                    boosterButton.MakeButtonUnavailable();
                else if (boosterButton.GetButtonBoosterType() == BoosterType.Sapper &&
                         _levelCreator.IsSpecialCardsOnLevel())
                    boosterButton.MakeButtonAvailable();
            }
        }
    }
}