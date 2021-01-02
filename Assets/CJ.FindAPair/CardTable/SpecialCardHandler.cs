using System.Linq;
using CJ.FindAPair.Constants;
using CJ.FindAPair.Game;
using UnityEngine;

namespace CJ.FindAPair.CardTable
{
    [RequireComponent(typeof(GameWatcher), typeof(CardComparator))]
    public class SpecialCardHandler : MonoBehaviour
    {
        private GameWatcher _gameWatcher;
        private CardComparator _cardComparator;

        private void Awake()
        {
            _gameWatcher = GetComponent<GameWatcher>();
            _cardComparator = GetComponent<CardComparator>();
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
                    OpenFortuneCard(card);
                    break;
                case ConstantsCard.NUMBER_ENTANGLEMENT:
                    OpenEntanglementCard();
                    break;
                case ConstantsCard.NUMBER_RESET:
                    OpenResetCard();
                    break;
                case ConstantsCard.NUMBER_BOMB:
                    OpenBombCard();
                    break;
            }
        }

        private void OpenFortuneCard(Card fortuneCard) //TODO Отрефакторить
        {
            var randomChance = Random.Range(0, 2);
            var allCards = FindObjectsOfType<Card>();

            var randomCard = allCards[Random.Range(0, allCards.Length)];

            if (randomChance > 0) //TODO Открытие пары карт (работает)
            {
                while (randomCard.IsMatched || randomCard.NumberPair >= ConstantsCard.NUMBER_SPECIAL)
                {
                    randomCard = allCards[Random.Range(0, allCards.Length)];
                }

                foreach (var card in allCards)
                {
                    if (card.NumberPair != randomCard.NumberPair) 
                        continue;
                    
                    card.Show();
                }
            }
            else //TODO Закрытие пары карт (забрать попытку, забрать очки, уменьшить кол-во угаданных пар)
            {
                var quantityMatchedCards = allCards.Count(card => card.IsMatched);

                if (quantityMatchedCards > 0)
                {
                    while (!randomCard.IsMatched || randomCard.NumberPair >= ConstantsCard.NUMBER_SPECIAL)
                    {
                        randomCard = allCards[Random.Range(0, allCards.Length)];
                    }

                    foreach (var card in allCards)
                    {
                        if (card.NumberPair != randomCard.NumberPair) 
                            continue;
                    
                        card.Hide();
                    }
                }
                else //TODO Если закрывать нечего, значит мы проиграли
                {
                    _gameWatcher.InitiateDefeat();
                }
            }

            fortuneCard.DelayHide();          
        }
        
        private void OpenEntanglementCard() //TODO Реализовать 
        {
        }

        private void OpenResetCard() //TODO Реализовать 
        {
        }

        private void OpenBombCard()
        {
            _gameWatcher.InitiateDefeat();
        }
    }
}