using CJ.FindAPair.CardTable;
using CJ.FindAPair.Game;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.CoreGames.Installer
{
    public class CoreGameCreator : MonoBehaviour
    {
        private GameCamera _gameCamera;
        private LevelCreator _levelCreator;
        private GameWatcher _gameWatcher;
        private CardComparator _cardComparator;

        [Inject]
        public void Construct(GameCamera gameCamera, LevelCreator levelCreator, GameWatcher gameWatcher,
            CardComparator cardComparator)
        {
            _gameCamera = gameCamera;
            _levelCreator = levelCreator;
            _levelCreator.transform.SetParent(transform);
            _gameWatcher = gameWatcher;
            _gameWatcher.transform.SetParent(transform);
            _cardComparator = cardComparator;
            _cardComparator.transform.SetParent(transform);
        }
    }
}