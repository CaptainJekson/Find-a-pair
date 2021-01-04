using CJ.FindAPair.CardTable;
using UnityEngine;

namespace CJ.FindAPair.Game.SpecialCards
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