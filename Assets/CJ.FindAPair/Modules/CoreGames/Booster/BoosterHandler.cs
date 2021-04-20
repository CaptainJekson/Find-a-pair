using System;
using System.Collections.Generic;
using System.Linq;
using CJ.FindAPair.CoreGames;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.CoreGames.Booster
{
    public class BoosterHandler : MonoBehaviour
    {
        [SerializeField] private List<Booster> _handlers;
        
        private GameWatcher _gameWatcher;
        private CardComparator _cardComparator;
        private LevelCreator _levelCreator;
        private Booster _booster;

        [Inject]
        public void Construct(LevelCreator levelCreator, GameWatcher gameWatcher, CardComparator cardComparator)
        {
            _levelCreator = levelCreator;
            _gameWatcher = gameWatcher;
            _cardComparator = cardComparator;
        }

        //TODO Подписывать кнопку соответствующего бустера
        public void BoosterActivationHandler(BoosterType boosterType)
        {
            switch (boosterType)
            {
                case BoosterType.MagicEye:
                    _booster = GetBooster<MagicEyeBooster>();
                    break;
                case BoosterType.Electroshock:
                    _booster = GetBooster<ElectroshockBooster>();
                    break;
                case BoosterType.Sapper:
                    _booster = GetBooster<SapperBooster>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(boosterType), boosterType, null);
            }
            
            if (_booster == null) return;
            _booster.Init(_gameWatcher, _levelCreator);
            _booster.ActivateBooster();
            _booster.DecreaseCount(boosterType);
        }
        
        private Booster GetBooster<T>() where T : Booster
        {
            foreach (var handler in _handlers.OfType<T>())
                _booster = handler;
            return _booster;
        }
    }
}