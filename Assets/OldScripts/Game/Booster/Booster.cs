using CJ.FindAPair.CardTable;
using CJ.FindAPair.CoreGames;
using UnityEngine;

namespace CJ.FindAPair.Game.Booster
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