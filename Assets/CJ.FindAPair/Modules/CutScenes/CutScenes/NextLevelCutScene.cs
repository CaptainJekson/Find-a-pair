using System;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.CutScenes.CutScenes.Base;
using CJ.FindAPair.Modules.UI;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;
using DG.Tweening;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes
{
    public class NextLevelCutScene : AbstractCutScene
    {
        private LevelConfigCollection _levelConfigCollection;
        private ISaver _gameSaver;
        private UIRoot _uiRoot;
        private LevelMapWindow _levelMapWindow;
        private LevelMarker _levelMarker;
        private bool _isNotOpenPreviewWindowNextTime;
            
        public event Action MoveMarkerComplete;
    
        public NextLevelCutScene(ISaver gameSaver, LevelConfigCollection levelConfigCollection, UIRoot uiRoot, 
            LevelMarker levelMarker)
        {
            _levelConfigCollection = levelConfigCollection;
            _gameSaver = gameSaver;
            _uiRoot = uiRoot;
            _levelMapWindow = uiRoot.GetWindow<LevelMapWindow>();
            _levelMarker = levelMarker;
        }

        public void NotOpenPreviewWindowNextTime()
        {
            _isNotOpenPreviewWindowNextTime = true;
        }
    
        public override void Play()
        {
            _levelMapWindow.StartCutSceneAtOpening = false;
            
            if(_gameSaver.LoadData().CurrentLevel >= _levelConfigCollection.Levels.Count + 1)
                return;
            
            var nextButton = _levelMapWindow.GetCurrentLocationAndButton().Value;
            nextButton.SetLockState();
            _uiRoot.OpenWindow<FullBlockerWindow>();

            var sequence = DOTween.Sequence();
            sequence.AppendInterval(0.5f);
            sequence.AppendCallback(() => _levelMarker.MoveToNextLevelButton(OnExplosionOccurred, OnMoveComplete));

            void OnExplosionOccurred()
            {
                nextButton.SetUnlockState();
            }
            
            void OnMoveComplete()
            {
                MoveMarkerComplete?.Invoke();
                
                _uiRoot.CloseWindow<FullBlockerWindow>();

                if (_isNotOpenPreviewWindowNextTime == false)
                {
                    nextButton.OpenPreviewWindow();
                }
                else
                {
                    _isNotOpenPreviewWindowNextTime = false;
                }
            }
        }

        public override void Stop() {}
    }
}