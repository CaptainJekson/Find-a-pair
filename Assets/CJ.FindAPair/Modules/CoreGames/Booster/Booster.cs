using UnityEngine;

namespace CJ.FindAPair.Modules.CoreGames.Booster
{
    public abstract class Booster : MonoBehaviour
    {
        protected LevelCreator _levelCreator;
        
        public void Init(LevelCreator levelCreator)
        {
            _levelCreator = levelCreator;
        }

        public abstract void ActivateBooster();
    }
}