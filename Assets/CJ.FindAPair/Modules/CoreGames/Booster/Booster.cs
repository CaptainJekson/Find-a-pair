using CJ.FindAPair.CoreGames;
using CJ.FindAPair.Game;
using UnityEngine;

namespace CJ.FindAPair.Modules.CoreGames.Booster
{
    public abstract class Booster : MonoBehaviour
    {
        protected GameWatcher _gameWatcher;
        protected LevelCreator _levelCreator;
        
        public void Init(GameWatcher gameWatcher, LevelCreator levelCreator )
        {
            _gameWatcher = gameWatcher;
            _levelCreator = levelCreator;
        }

        public void DecreaseCount(BoosterType boosterType)
        {
            GameSaver.SaveBooster(boosterType, -1);
        }
        
        public abstract void ActivateBooster();
    }
}