using System.Collections.Generic;
using CJ.FindAPair.Constants;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.CutScene.CutScenes;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

namespace CJ.FindAPair.Modules.CoreGames
{
    public class LevelCreator : MonoBehaviour
    {
        [SerializeField] private Card _card;

        private CardsPlacer _cardsPlacer;
        private LevelConfig _level;
        private UIRoot _uiRoot;
        private LevelRewardCutScene _levelRewardCutScene;
        private ScoreObtainCutScene _scoreObtainCutScene;
        private List<Card> _cards;
        private List<Card> _disableCards;

        public float Scale => _level.HeightOffset;
        public LevelConfig LevelConfig => _level;
        public List<Card> Cards => _cards;

        public event UnityAction OnLevelCreated;
        public event UnityAction OnLevelDeleted;

        [Inject]
        public void Construct(CardsPlacer cardsPlacer, UIRoot uiRoot, LevelRewardCutScene levelRewardCutScene, 
            ScoreObtainCutScene scoreObtainCutScene)
        {
            _cardsPlacer = cardsPlacer;
            _uiRoot = uiRoot;
            _levelRewardCutScene = levelRewardCutScene;
            _scoreObtainCutScene = scoreObtainCutScene;
            _cards = new List<Card>();
            _disableCards = new List<Card>();
        }

        public void CreateLevel(LevelConfig level)
        {
            _level = level;
            _uiRoot.OpenWindow<FullBlockerWindow>();
            
            PlaceCards();
            CardNumbering();
            AddAllSpecialCards();
            ShuffleNumberCard();
            _cardsPlacer.DealCards(_cards);
            _scoreObtainCutScene.PrepareCutScene();

            OnLevelCreated?.Invoke();
        }

        public void ClearLevel()
        {
            foreach (var card in _cards)
            {
                Destroy(card.gameObject);
            }

            foreach (var card in _disableCards)
            {
                Destroy(card.gameObject);
            }

            _cards.Clear();
            _disableCards.Clear();
            _levelRewardCutScene.Stop();
            _scoreObtainCutScene.Stop();

            OnLevelDeleted?.Invoke();
        }

        public void RestartLevel()
        {
            ClearLevel();
            CreateLevel(_level);
        }

        private void PlaceCards()
        {
            var cards = _cardsPlacer.PlaceCards(_level, _card, transform);

            foreach (var card in cards)
            {
                AddCreatedCardToList(card.Value, card.Key);
            }
        }

        private void AddCreatedCardToList(bool isFilledCell, Card newCard)
        {
            if (isFilledCell == false)
            {
                DisableCard(newCard);
                _disableCards.Add(newCard);
            }
            else
            {
                _cards.Add(newCard);
            }
        }

        private void CardNumbering()
        {
            var numberCard = 1;
            var counter = (int) _level.QuantityOfCardOfPair;

            for (var i = 0; i < _cards.Count; i++)
            {
                if (counter > 0)
                {
                    _cards[i].NumberPair = numberCard;
                    --counter;
                }
                else
                {
                    counter = (int) _level.QuantityOfCardOfPair;
                    ++numberCard;
                    --i;
                }
            }
        }

        private void ShuffleNumberCard()
        {
            for (var i = _cards.Count - 1; i > 0; i--)
            {
                var j = Random.Range(0, i);
                var temp = _cards[i].NumberPair;
                _cards[i].NumberPair = _cards[j].NumberPair;
                _cards[j].NumberPair = temp;
            }
        }

        private void AddAllSpecialCards()
        {
            AddSpecialCards(_level.QuantityPairOfFortune, ConstantsCard.NUMBER_FORTUNE);
            AddSpecialCards(_level.QuantityPairOfEntanglement, ConstantsCard.NUMBER_ENTANGLEMENT);
            AddSpecialCards(_level.QuantityPairOfReset, ConstantsCard.NUMBER_RESET);
            AddSpecialCards(_level.QuantityPairOfBombs, ConstantsCard.NUMBER_BOMB);
        }

        private void AddSpecialCards(int quantityPairOfSpecial, int number)
        {
            var quantitySpecialCards = quantityPairOfSpecial * (int) _level.QuantityOfCardOfPair;

            for (var i = 0; i < quantitySpecialCards; i++)
            {
                _cards[_cards.Count - 1 - i].NumberPair = number;
            }
        }

        private void DisableCard(Card card)
        {
            card.IsEmpty = true;
            card.MakeEmpty();
            card.NumberPair = 0;
        }

        public bool IsSpecialCardsOnLevel()
        {
            foreach (var card in _cards)
            {
                if (card.NumberPair >= ConstantsCard.NUMBER_SPECIAL && !card.IsMatched)
                    return true;
            }

            return false;
        }
    }
}