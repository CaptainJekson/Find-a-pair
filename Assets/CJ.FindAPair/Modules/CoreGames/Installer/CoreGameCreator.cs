﻿using CJ.FindAPair.Modules.CoreGames.Booster;
using CJ.FindAPair.Modules.CoreGames.SpecialCards;
using CJ.FindAPair.Modules.UI.Installer;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.CoreGames.Installer
{
    public class CoreGameCreator : MonoBehaviour
    {
        [SerializeField] private Transform _tableCanvas;
        private LevelCreator _levelCreator;
        private GameWatcher _gameWatcher;
        private RayCaster _rayCaster;
        private CardComparator _cardComparator;
        private BoosterHandler _boosterHandler;
        private SpecialCardHandler _specialCardHandler;
        private LevelBackground _levelBackground;
        private UIRoot _uiRoot;

        [Inject]
        public void ConstructCoreGame(LevelCreator levelCreator, GameWatcher gameWatcher, RayCaster rayCaster,
            CardComparator cardComparator, BoosterHandler boosterHandler, SpecialCardHandler specialCardHandler,
            LevelBackground levelBackground, UIRoot uiRoot)
        {
            _levelBackground = levelBackground;
            _levelCreator = levelCreator;
            _levelCreator.transform.SetParent(transform);
            _gameWatcher = gameWatcher;
            _gameWatcher.transform.SetParent(transform);
            _rayCaster = rayCaster;
            _rayCaster.transform.SetParent(transform);
            _cardComparator = cardComparator;
            _boosterHandler = boosterHandler;
            _boosterHandler.transform.SetParent(transform);
            _specialCardHandler = specialCardHandler;
            _specialCardHandler.transform.SetParent(transform);
        }

        private void SetCanvasPosition(Transform transform)
        {
            transform.position = _tableCanvas.position;
            transform.SetParent(_tableCanvas);
        }
    }
}