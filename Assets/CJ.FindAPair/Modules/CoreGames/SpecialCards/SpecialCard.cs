using CJ.FindAPair.CoreGames;
using UnityEngine;

namespace CJ.FindAPair.Modules.CoreGames.SpecialCards
{
    public abstract class SpecialCard : MonoBehaviour
    {
        protected GameWatcher _gameWatcher;
        protected LevelCreator _levelCreator;
        
        public void Init(GameWatcher gameWatcher, LevelCreator levelCreator )
        {
            _gameWatcher = gameWatcher;
            _levelCreator = levelCreator;
        }
        
        public abstract void OpenSpecialCard(Card specialCard);
    }
}