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
        [SerializeField] private ScrollRect _scrollRect;
        
        private LevelConfigCollection _levelConfigCollection;
        private LevelCreator _levelCreator;
        private UIRoot _uiRoot;
        private LevelBackground _levelBackground;
        private ISaver _gameSaver;
        
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
            
            _spawnedLevelLocation = new List<LevelLocation>();
            _levelButtons = new List<LevelButton>();
        }

        protected override void Init()
        {
            CreateLocations();
            SetLevelButtons();
            SetLevelData();
        }

        protected override void OnOpen()
        {
            RefreshLevelButtons();
            _levelBackground.gameObject.SetActive(false);
            SetStartScrollPosition();
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

        //TODO dev
        private void PlayNextLevelCutScene()
        {
            
        }

        private void MoveToCurrentLevel(float duration = 0)
        {
            var currentLevel = _gameSaver.LoadData().CurrentLevel;
            var levelIndex = currentLevel - 1;
            var targetButton = _levelButtons[levelIndex].GetComponent<RectTransform>();
            
            Canvas.ForceUpdateCanvases();

            var targetPosition = (Vector2) _scrollRect.transform.InverseTransformPoint(_contentPosition.position)
                                 - (Vector2) _scrollRect.transform.InverseTransformPoint(targetButton.position);
            _contentPosition.DOAnchorPos(targetPosition, duration);
        }
    }
}
