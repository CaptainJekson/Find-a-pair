using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Slots;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class LevelMapWindow : Window
    {
        [SerializeField] private List<LevelLocation> _levelLocations;
        [SerializeField] private RectTransform _contentPosition;
        [SerializeField] private Scrollbar _scrollbar;
        
        private LevelConfigCollection _levelConfigCollection;
        private LevelCreator _levelCreator;
        private UIRoot _uiRoot;
        private LevelBackground _levelBackground;
        private ISaver _gameSaver;
        private GameWatcher _gameWatcher;
        
        private List<LevelButton> _levelButtons;

        private List<LevelLocation> _spawnedLevelLocation;

        [Inject]
        private void Construct(LevelConfigCollection levelConfigCollection, LevelCreator levelCreator, UIRoot uiRoot,
            LevelBackground levelBackground, ISaver gameSaver, GameWatcher gameWatcher)
        {
            _levelConfigCollection = levelConfigCollection;
            _levelCreator = levelCreator;
            _uiRoot = uiRoot;
            _levelBackground = levelBackground;
            _gameSaver = gameSaver;
            _gameWatcher = gameWatcher;
            
            _spawnedLevelLocation = new List<LevelLocation>();
            _levelButtons = new List<LevelButton>();
        }
        
        protected override void Init()
        {
            CreateLocations();
            SetLevelButtons();
            SetLevelData();

            //_gameWatcher.ThereWasAVictory += RefreshLevelButtons;
            DOTween.To(()=> _scrollbar.value, x=> _scrollbar.value = x, 0.0f, 2.0f);
        }
        
        protected override void OnOpen()
        {
            RefreshLevelButtons();
            _levelBackground.gameObject.SetActive(false);
        }

        protected override void OnClose()
        {
            _levelBackground.gameObject.SetActive(true);
        }

        private void CreateLocations()
        {
            foreach (var location in _levelLocations)
            {
                var spawnedLocation = Instantiate(location, _contentPosition);
                spawnedLocation.transform.SetParent(_contentPosition, false);
                spawnedLocation.transform.SetAsFirstSibling();
                _spawnedLevelLocation.Add(spawnedLocation);
            }
        }

        private void SetLevelButtons()
        {
            foreach (var location in _spawnedLevelLocation)
            {
                _levelButtons.AddRange(location.LevelButtons);
            }
        }

        private void SetLevelData()
        {
            for (var i = 0; i < _levelConfigCollection.Levels.Count; i++)
            {
                _levelButtons[i].SetData(_levelConfigCollection.Levels[i], _levelCreator, _uiRoot, _gameSaver);
                _levelButtons[i].SetStateButton();
            }
        }

        private void RefreshLevelButtons()
        {
            for (var i = 0; i < _levelConfigCollection.Levels.Count; i++)
            {
                _levelButtons[i].SetStateButton();
            }
        }
    }
}
