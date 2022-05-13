using System;
using CJ.FindAPair.Modules.CutScenes.CutScenes.Base;
using CJ.FindAPair.Modules.UI.View;
using DG.Tweening;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes
{
    public class UnlockLocationCutScene : AbstractCutScene
    {
        private LevelLocation _levelLocation;
        
        public override event Action CutSceneComplete;

        public void SetLocation(LevelLocation levelLocation)
        {
            _levelLocation = levelLocation;
        }

        public override void Play()
        {
            //TODO скролл туда сюда нужно ещё реализовать
            _levelLocation.Unlock();
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(1.0f);
            sequence.AppendCallback(() => CutSceneComplete?.Invoke());
        }

        public override void Stop()
        {
            
        }
    }
}