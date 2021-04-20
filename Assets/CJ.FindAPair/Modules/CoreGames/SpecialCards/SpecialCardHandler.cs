using System.Collections.Generic;
using System.Linq;
using CJ.FindAPair.Constants;
using CJ.FindAPair.CoreGames;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.CoreGames.SpecialCards
{
    public class SpecialCardHandler : MonoBehaviour
    {
        [SerializeField] private List<SpecialCard> _handlers;
        
        private GameWatcher _gameWatcher;
        private CardComparator _cardComparator;
        private LevelCreator _levelCreator;

        private SpecialCard _specialCard;

        [Inject]
        public void Construct(LevelCreator levelCreator, GameWatcher gameWatcher, CardComparator cardComparator)
        {
            _levelCreator = levelCreator;
            _gameWatcher = gameWatcher;
            _cardComparator = cardComparator;
        }

        private void OnEnable()
        {
            _cardComparator.SpecialCardOpened += SpecialCardOpeningHandler;
        }

        private void OnDisable()
        {
            _cardComparator.SpecialCardOpened -= SpecialCardOpeningHandler;
        }

        private void SpecialCardOpeningHandler(Card card)
        {
            switch (card.NumberPair)
            {
                case ConstantsCard.NUMBER_FORTUNE:
                    _specialCard = GetSpecialCard<FortuneCard>();
                    break;
                case ConstantsCard.NUMBER_ENTANGLEMENT:
                    _specialCard = GetSpecialCard<EntanglementCard>();
                    break;
                case ConstantsCard.NUMBER_RESET:
                    _specialCard = GetSpecialCard<ResetCard>();
                    break;
                case ConstantsCard.NUMBER_BOMB:
                    _specialCard = GetSpecialCard<BombCard>();
                    break;
            }

            if (_specialCard == null) return;
            _specialCard.Init(_gameWatcher, _levelCreator);
            _specialCard.OpenSpecialCard(card);
        }

        private SpecialCard GetSpecialCard<T>() where T : SpecialCard
        {
            foreach (var handler in _handlers.OfType<T>())
                _specialCard = handler;
            return _specialCard;
        }
    }
}