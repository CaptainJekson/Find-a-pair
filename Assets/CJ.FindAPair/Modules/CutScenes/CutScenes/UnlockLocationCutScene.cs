﻿using System;
using CJ.FindAPair.Modules.CutScenes.CutScenes.Base;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;
using DG.Tweening;
using UnityEngine;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes
{
    public class UnlockLocationCutScene : AbstractCutScene
    {
        private LevelMapWindow _levelMapWindow;
        
        public override event Action CutSceneComplete;

        public UnlockLocationCutScene(UIRoot uiRoot)
        {
            _levelMapWindow = uiRoot.GetWindow<LevelMapWindow>();
        }

        public override void Play()
        {
            var currentLocation = _levelMapWindow.GetCurrentLocationAndButton().Key;
            var levelButtons = currentLocation.LevelButtons;

            foreach (var levelButton in levelButtons)
            {
                levelButton.gameObject.SetActive(false);
            }
            
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => _levelMapWindow.MoveToCurrentLocation(1.0f));
            sequence.AppendInterval(1.0f);
            sequence.AppendCallback(() => currentLocation.Unlock());
            sequence.AppendInterval(1.0f);

            foreach (var levelButton in levelButtons)
            {
                sequence.AppendCallback(() => levelButton.gameObject.SetActive(true));
                sequence.Append(levelButton.transform.DOScale(Vector3.one, 0.05f)
                    .From(Vector3.zero).SetEase(Ease.OutBack));
            }
            
            sequence.AppendCallback(() => _levelMapWindow.MoveToCurrentLevel(1.0f));
            sequence.AppendInterval(1.0f);
            sequence.AppendCallback(() => CutSceneComplete?.Invoke());
        }

        public override void Stop()
        {
            
        }
    }
}