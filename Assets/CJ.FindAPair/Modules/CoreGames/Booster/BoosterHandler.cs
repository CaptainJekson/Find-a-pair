using System;
using System.Collections.Generic;
using System.Linq;
using CJ.FindAPair.Modules.Service.Audio;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.CoreGames.Booster
{
    public class BoosterHandler : MonoBehaviour
    {
        [SerializeField] private List<Booster> _handlers;
        
        private CardComparator _cardComparator;
        private LevelCreator _levelCreator;
        private Booster _booster;
        private AudioController _audioController;

        [Inject]
        public void Construct(LevelCreator levelCreator, CardComparator cardComparator, AudioController audioController)
        {
            _levelCreator = levelCreator;
            _cardComparator = cardComparator;
            _audioController = audioController;
        }
        
        public void BoosterActivationHandler(BoosterType boosterType)
        {
            switch (boosterType)
            {
                case BoosterType.Detector:
                    _booster = GetBooster<DetectorBooster>();
                    break;
                case BoosterType.Magnet:
                    _booster = GetBooster<MagnetBooster>();
                    break;
                case BoosterType.Sapper:
                    _booster = GetBooster<SapperBooster>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(boosterType), boosterType, null);
            }
            
            if (_booster == null) return;
            _booster.Init(_levelCreator, _audioController);
            _booster.ActivateBooster();
            //_booster.DecreaseCount(boosterType);
        }
        
        private Booster GetBooster<T>() where T : Booster
        {
            foreach (var handler in _handlers.OfType<T>())
                _booster = handler;
            return _booster;
        }
    }
}