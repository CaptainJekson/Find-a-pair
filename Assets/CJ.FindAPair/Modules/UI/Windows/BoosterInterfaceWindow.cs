using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Booster;
using CJ.FindAPair.Modules.CoreGames.SpecialCards;
using CJ.FindAPair.Modules.UI.View;
using CJ.FindAPair.Modules.UI.Windows.Base;
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
        private SpecialCardHandler _specialCardHandler;
        private ISaver _gameSaver;

        [Header("Positions for tutorial")] 
        [SerializeField] private RectTransform _detectorTransform;
        [SerializeField] private RectTransform _magnetTransform;
        [SerializeField] private RectTransform _sapperTransform;
        
        public RectTransform DetectorTransform => _detectorTransform;
        public RectTransform MagnetTransform => _magnetTransform;
        public RectTransform SapperTransform => _sapperTransform;

        [Inject]

        public void Construct(BoosterHandler boosterHandler, ISaver gameSaver, LevelCreator levelCreator, 
            SpecialCardHandler specialCardHandler)
        {
            _boosterHandler = boosterHandler;
            _gameSaver = gameSaver;
            _levelCreator = levelCreator;
            _specialCardHandler = specialCardHandler;
        }

        protected override void OnOpen()
        {
            TryDisableSapperButton();
            RefreshButtons();
            _specialCardHandler.SpecialCardOpened += TryDisableSapperButton;
        }

        protected override void OnClose()
        {
            _specialCardHandler.SpecialCardOpened -= TryDisableSapperButton;
        }

        protected override void PlayOpenSound()
        {
        }

        protected override void PlayCloseSound()
        {
        }

        private void RefreshButtons()
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
                if (boosterButton.CanCooldown)
                    boosterButton.TryActivateCooldown(_boosterCooldownTime);
            }
        }

        public void TryDisableSapperButton()
        {
            foreach (var boosterButton in _boosterButtons)
            {
                if (!boosterButton.CanCooldown && !_levelCreator.IsSpecialCardsOnLevel())
                    boosterButton.MakeButtonUnavailable();
                else if (!boosterButton.CanCooldown && _levelCreator.IsSpecialCardsOnLevel() && boosterButton.GetBoosterSaveData() > 0)
                    boosterButton.MakeButtonAvailable();
            }
        }
    }
}