using CJ.FindAPair.Modules.CoreGames.Booster;
using CJ.FindAPair.Modules.CoreGames.SpecialCards;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.CoreGames.Installer
{
    public class CoreGameCreator : MonoBehaviour
    {
        [SerializeField] private Transform _tableCanvas;
        
        private LevelCreator _levelCreator;
        private GameWatcher _gameWatcher;
        private CardComparator _cardComparator;
        private BoosterHandler _boosterHandler;
        private SpecialCardHandler _specialCardHandler;
        private LevelBackground _levelBackground;
       // private CreateLevelTEST _createLevelTest;

        [Inject]
        public void Construct(LevelCreator levelCreator, GameWatcher gameWatcher,
            CardComparator cardComparator, BoosterHandler boosterHandler, SpecialCardHandler specialCardHandler,
            LevelBackground levelBackground
            /*,CreateLevelTEST createLevelTest*/)
        {
            _levelCreator = levelCreator;
            SetCanvasPosition(_levelCreator.transform);
            _levelBackground = levelBackground;
            SetCanvasPosition(_levelBackground.transform);
            _gameWatcher = gameWatcher;
            _gameWatcher.transform.SetParent(transform);
            _cardComparator = cardComparator;
            _cardComparator.transform.SetParent(transform);
            _boosterHandler = boosterHandler;
            _boosterHandler.transform.SetParent(transform);
            _specialCardHandler = specialCardHandler;
            _specialCardHandler.transform.SetParent(transform);
            //_createLevelTest = createLevelTest;
        }

        private void SetCanvasPosition(Transform transform)
        {
            transform.position = _tableCanvas.position;
            transform.SetParent(_tableCanvas);
        }
    }
}