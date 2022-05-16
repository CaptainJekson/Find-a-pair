using System;
using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.CutScenes.CutScenes;
using CJ.FindAPair.Modules.CutScenes.CutScenes.Base;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.View;
using CJ.FindAPair.Modules.UI.Windows.Base;
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
        private UnlockLocationCutScene _unlockLocationCutScene;
        private ProgressGiftBoxSaver _progressGiftBoxSaver;
        private GiftBoxCutScene _giftBoxCutScene;
        private NextLevelCutScene _nextLevelCutScene;
        private QueueCutScenes _queueCutScenes;
        private Dictionary<LevelLocation, List<LevelButton>> _levelLocationsWithLevelButtons;
        private int _currentLevel;

        [Inject]
        private void Construct(LevelConfigCollection levelConfigCollection, LevelCreator levelCreator, UIRoot uiRoot,
            LevelBackground levelBackground, ISaver gameSaver, UnlockLocationCutScene unlockLocationCutScene,
            ProgressGiftBoxSaver progressGiftBoxSaver, GiftBoxCutScene giftBoxCutScene, 
            NextLevelCutScene nextLevelCutScene, QueueCutScenes queueCutScenes)
        {
            _levelConfigCollection = levelConfigCollection;
            _levelCreator = levelCreator;
            _uiRoot = uiRoot;
            _levelBackground = levelBackground;
            _gameSaver = gameSaver;
            _unlockLocationCutScene = unlockLocationCutScene;
            _giftBoxCutScene = giftBoxCutScene;
            _nextLevelCutScene = nextLevelCutScene;
            _queueCutScenes = queueCutScenes;
            _progressGiftBoxSaver = progressGiftBoxSaver;
            _levelLocationsWithLevelButtons = new Dictionary<LevelLocation, List<LevelButton>>();
        }

        protected override void Init()
        {
            CreateLocations();
            SetLevelData();
            UnlockCompletedLocation();
            _currentLevel = _gameSaver.LoadData().CurrentLevel;
        }

        protected override void OnOpen()
        {
            _uiRoot.OpenWindow<MenuButtonsWindow>();
            
            RefreshLevelButtons();
            PlayCutScenesIfNeed();
            
            _levelBackground.gameObject.SetActive(false);
            SetStartScrollPosition();
        }

        protected override void OnClose()
        {
            _audioController.StopMusic();
            _uiRoot.CloseWindow<MenuButtonsWindow>();
            
            if (_levelBackground != null)
            {
                _levelBackground.gameObject.SetActive(true);
            }
        }

        protected override void PlayOpenSound()
        {
        }

        protected override void PlayCloseSound()
        {
        }

        public KeyValuePair<LevelLocation, LevelButton> GetCurrentLocationAndButton()
        {
            var currentLevel = _gameSaver.LoadData().CurrentLevel;

            foreach (var levelLocation in _levelLocationsWithLevelButtons)
            {
                foreach (var levelButton in levelLocation.Value)
                {
                    if (levelButton.LevelNumber == currentLevel)
                    {
                        return new KeyValuePair<LevelLocation, LevelButton>(levelLocation.Key, levelButton);
                    }
                }
            }
            
            throw new Exception("[There is no button with this current level]");
        }

        public void MoveToCurrentLocation(float duration = 0)
        {
            var currentLocation = GetCurrentLocationAndButton().Key;
            
            Canvas.ForceUpdateCanvases();

            var targetPosition = (Vector2) _scrollRect.transform.InverseTransformPoint(_contentPosition.position)
                                 - (Vector2) _scrollRect.transform.InverseTransformPoint(currentLocation.transform.position);
            _contentPosition.DOAnchorPos(targetPosition, duration);
        }
        
        public void MoveToCurrentLevel(float duration = 0)
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

        private void UnlockCompletedLocation()
        {
            var currentLevel = _gameSaver.LoadData().CurrentLevel;

            foreach (var levelLocation in _levelLocationsWithLevelButtons)
            {
                levelLocation.Key.UnlockFast();
                
                foreach (var levelButton in levelLocation.Value)
                {
                    if (levelButton.LevelNumber == currentLevel)
                    {
                        return;
                    }
                }
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

        private List<LevelButton> GetAllButtons()
        {
            var levelButtons = new List<LevelButton>();

            foreach (var levelLocationWithLevelButtons in _levelLocationsWithLevelButtons)
            {
                levelButtons.AddRange(levelLocationWithLevelButtons.Value);
            }

            return levelButtons;
        }

        private void PlayCutScenesIfNeed()
        {
            var currentLocation = GetCurrentLocationAndButton().Key;
            if (currentLocation.IsUnlock == false)
            {
                _queueCutScenes.AddQueue(_unlockLocationCutScene);
            }
            
            if (_progressGiftBoxSaver.IsSaveProgress && 
                _levelConfigCollection.Levels[_levelCreator.LevelConfig.LevelNumber - 1].RewardItemsCollection)
            {
                _queueCutScenes.AddQueue(_giftBoxCutScene);
                _progressGiftBoxSaver.IsSaveProgress = false;
            }
            
            if (_gameSaver.LoadData().CurrentLevel > _currentLevel)
            {
                _queueCutScenes.AddQueue(_nextLevelCutScene);
                _currentLevel++;
            }

            _queueCutScenes.ExecuteQueue();
        }
    }
}