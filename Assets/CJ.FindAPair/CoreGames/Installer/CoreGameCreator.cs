using CJ.FindAPair.CardTable;
using CJ.FindAPair.CoreGames.SpecialCards;
using CJ.FindAPair.CoreGames.TEST;
using CJ.FindAPair.Game;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.CoreGames.Installer
{
    public class CoreGameCreator : MonoBehaviour
    {
        [SerializeField] private RectTransform _tableCanvas;
        
        private LevelCreator _levelCreator;
        private GameWatcher _gameWatcher;
        private CardComparator _cardComparator;
        private BoosterHandler _boosterHandler;
        private SpecialCardHandler _specialCardHandler;
        private CreateLevelTEST _createLevelTest;

        [Inject]
        public void Construct(LevelCreator levelCreator, GameWatcher gameWatcher,
            CardComparator cardComparator, BoosterHandler boosterHandler, SpecialCardHandler specialCardHandler,
            CreateLevelTEST createLevelTest)
        {
            _levelCreator = levelCreator;
            SetCanvasPosition(_levelCreator.GetComponent<RectTransform>());
            _gameWatcher = gameWatcher;
            _gameWatcher.transform.SetParent(transform);
            _cardComparator = cardComparator;
            _cardComparator.transform.SetParent(transform);
            _boosterHandler = boosterHandler;
            _boosterHandler.transform.SetParent(transform);
            _specialCardHandler = specialCardHandler;
            _specialCardHandler.transform.SetParent(transform);
            _createLevelTest = createLevelTest;
        }

        private void SetCanvasPosition(RectTransform rectTransform)
        {
            rectTransform.position = _tableCanvas.position;
            rectTransform.SetParent(_tableCanvas);
        }
    }
}