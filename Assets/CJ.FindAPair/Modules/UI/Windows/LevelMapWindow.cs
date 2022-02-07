using System;
using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.CutScenes.CutScenes;
using CJ.FindAPair.Modules.Meta.Configs;
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
        [SerializeField] private ScrollRect _scrollRect;
        
        private LevelConfigCollection _levelConfigCollection;
        private LevelCreator _levelCreator;
        private UIRoot _uiRoot;
        private LevelBackground _levelBackground;
        private ISaver _gameSaver;
        private NextLevelCutScene _nextLevelCutScene;
        private GiftBoxWindow _giftBoxWindow;
        private AudioController _audioController;
        private ThemeConfigCollection _themeConfigCollection;
        private Dictionary<LevelLocation, List<LevelButton>> _levelLocationsWithLevelButtons;
        
        public bool StartCutSceneAtOpening { get; set; }

        public bool AbleGiftObtainAtOpen { get; set; }

        public bool IsScrollMove => Mathf.Abs(_scrollRect.velocity.y) > 100;

        [Inject]
        private void Construct(LevelConfigCollection levelConfigCollection, LevelCreator levelCreator, UIRoot uiRoot,
            LevelBackground levelBackground, ISaver gameSaver, NextLevelCutScene nextLevelCutScene, 
            AudioController audioController, ThemeConfigCollection themeConfigCollection)
        {
            _levelConfigCollection = levelConfigCollection;
            _levelCreator = levelCreator;
            _uiRoot = uiRoot;
            _levelBackground = levelBackground;
            _gameSaver = gameSaver;
            _nextLevelCutScene = nextLevelCutScene;
            _giftBoxWindow = uiRoot.GetWindow<GiftBoxWindow>();
            _audioController = audioController;
            _themeConfigCollection = themeConfigCollection;
            _levelLocationsWithLevelButtons = new Dictionary<LevelLocation, List<LevelButton>>();
        }

        protected override void Init()
        {
            CreateLocations();
            SetLevelData();
        }

        protected override void OnOpen()
        {
            _audioController.PlayMusic(_themeConfigCollection.GetThemeConfig(_gameSaver.LoadData()
                .ThemesData.SelectedTheme).Music);
            
            _giftBoxWindow.WindowClosed += TryStartNextLevelCutScene;
            _uiRoot.OpenWindow<MenuButtonsWindow>();
            
            RefreshLevelButtons();
            
            TryStartCutScenes();
            AbleGiftObtainAtOpen = false;
            
            _levelBackground.gameObject.SetActive(false);
            SetStartScrollPosition();
        }

        protected override void OnClose()
        {
            _audioController.StopMusic();
            
            _giftBoxWindow.WindowClosed -= TryStartNextLevelCutScene;
            _uiRoot.CloseWindow<MenuButtonsWindow>();
            
            if (_levelBackground != null)
            {
                _levelBackground.gameObject.SetActive(true);
            }
        }
        
        public KeyValuePair<LevelLocation, LevelButton> GetCurrentLocationAndButton()
        {
            var currentLevel = _gameSaver.LoadData().CurrentLevel;

            foreach (var levelLocation in _levelLocationsWithLevelButtons)
            {
                foreach (var levelButton in levelLocation.Value)
                {
                    if(levelButton.LevelNumber == currentLevel)
                        return new KeyValuePair<LevelLocation, LevelButton>(levelLocation.Key, levelButton);
                }
            }
            
            throw new Exception("[There is no button with this current level]");
        }

        private void CreateLocations()
        {
            foreach (var location in _levelLocations)
            {
                var spawnedLocation = Instantiate(location, _contentPosition);
                spawnedLocation.transform.SetParent(_contentPosition, false);
                spawnedLocation.transform.SetAsFirstSibling();
                _levelLocationsWithLevelButtons.Add(spawnedLocation, spawnedLocation.LevelButtons);
            }
        }

        private void SetLevelData()
        {
            var levelButtons = GetAllButtons();
            
            for (var i = 0; i < _levelConfigCollection.Levels.Count; i++)
            {
                levelButtons[i].SetData(_levelConfigCollection.Levels[i], _levelCreator, _uiRoot, _gameSaver);
            }
        }

        private void RefreshLevelButtons()
        {
            var levelButtons = GetAllButtons();
            
            for (var i = 0; i < _levelConfigCollection.Levels.Count; i++)
            {
                levelButtons[i].SetStateButton();
            }
        }
        
        private void SetStartScrollPosition()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(0.0f);
            sequence.AppendCallback(()=>
            {
                _scrollRect.verticalNormalizedPosition = 0.0f;
                MoveToCurrentLevel();
            });
        }

        private void MoveToCurrentLevel(float duration = 0)
        {
            var currentLevel = _gameSaver.LoadData().CurrentLevel;
            var levelIndex = currentLevel - 1;
            var levelButtons = GetAllButtons();
            var targetButton = levelButtons[levelIndex].GetComponent<RectTransform>();
            
            Canvas.ForceUpdateCanvases();

            var targetPosition = (Vector2) _scrollRect.transform.InverseTransformPoint(_contentPosition.position)
                                 - (Vector2) _scrollRect.transform.InverseTransformPoint(targetButton.position);
            _contentPosition.DOAnchorPos(targetPosition, duration);
        }
        
        private List<LevelButton> GetAllButtons()
        {
            var levelButtons = new List<LevelButton>();

            foreach (var levelLocationWithLevelButtons in _levelLocationsWithLevelButtons)
            {
                levelButtons.AddRange(levelLocationWithLevelButtons.Value);
            }

            return levelButtons;
        }

        private void TryStartCutScenes()
        {
            if (AbleGiftObtainAtOpen && 
                _levelConfigCollection.Levels[_levelCreator.LevelConfig.LevelNumber - 1].RewardItemsCollection)
            {
                _giftBoxWindow.Open();
            }
            else
            {
                TryStartNextLevelCutScene();
            }
        }

        private void TryStartNextLevelCutScene()
        {
            if (StartCutSceneAtOpening)
                _nextLevelCutScene.Play();
        }
    }
}