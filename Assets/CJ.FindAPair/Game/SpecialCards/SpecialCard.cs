using CJ.FindAPair.CardTable;
using UnityEngine;

namespace CJ.FindAPair.Game.SpecialCards
{
    public abstract class SpecialCard : MonoBehaviour
    {
        protected GameWatcher _gameWatcher;
        protected CardComparator _cardComparator;
        protected LevelCreator _levelCreator;
        
        public virtual void Init(GameWatcher gameWatcher, CardComparator cardComparator, LevelCreator levelCreator )
        {
            _gameWatcher = gameWatcher;
            _cardComparator = cardComparator;
            _levelCreator = levelCreator;
        }
        
        public abstract void OpenSpecialCard(Card card);
    }
}