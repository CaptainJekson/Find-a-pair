using System.Collections.Generic;
using System.Linq;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes.Base
{
    public class QueueCutScenes
    {
        private List<AbstractCutScene> _cutScenes;
        private AbstractCutScene _currentCutScene;

        public QueueCutScenes()
        {
            _cutScenes = new List<AbstractCutScene>();
        }

        public void AddQueue(AbstractCutScene cutScene)
        {
            _cutScenes.Add(cutScene);
        }

        public void ExecuteQueue()
        {
            if (_cutScenes.Count == 0)
            {
                return;
            }
            
            _currentCutScene = _cutScenes.First();
            _currentCutScene.Play();
            _currentCutScene.CutSceneComplete += OnCutSceneComplete;
        }

        private void OnCutSceneComplete()
        {
            _currentCutScene.CutSceneComplete -= OnCutSceneComplete;
            _cutScenes.Remove(_currentCutScene);
            ExecuteQueue();
        }
    }
}