using UnityEngine;

namespace CJ.FindAPair.Modules.CoreGames.Booster
{
    public abstract class Booster : MonoBehaviour
    {
        protected LevelCreator _levelCreator;
        protected AudioController _audioController;
        
        public void Init(LevelCreator levelCreator, AudioController audioController)
        {
            _levelCreator = levelCreator;
            _audioController = audioController;
        }

        public abstract void ActivateBooster();
    }
}